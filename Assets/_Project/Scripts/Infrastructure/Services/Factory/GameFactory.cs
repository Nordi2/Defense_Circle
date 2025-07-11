using _Project.Cor.Component;
using _Project.Cor.Enemy;
using _Project.Cor.Enemy.Mono;
using _Project.Cor.Interfaces;
using _Project.Cor.Observers;
using _Project.Cor.Tower;
using _Project.Cor.Tower.Animation;
using _Project.Cor.Tower.Mono;
using _Project.Data.Config;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using _Project.Meta.StatsLogic.NoneUpgrade;
using _Project.Meta.StatsLogic.Upgrade;
using _Project.Scripts.Gameplay.Component;
using Infrastructure.Services;
using Infrastructure.Services.Services.LoadData;
using Infrastructure.Services.Services.ScreenResolution;
using System;
using _Project.Cor.Spawner;
using _Project.Data.Config.Stats;
using _Project.Infrastructure.AssetManagement;
using _Project.Infrastructure.Services.AssetManagement;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Services
{
    [UsedImplicitly]
    public class GameFactory :
        IGameFactory
    {
        private const float Padding = 10;
        private const string GameplayObjectParent = "[GameplayObject]";
        private const string PoolsObject = "[PoolsObject]";
        private const string EnemiesPool = "[EnemiesPool]";

        private readonly IInstantiator _instantiator;
        private readonly IGetTargetPosition _getTargetPosition;
        private readonly IGameLoadDataService _gameLoadDataService;
        private readonly IScreenResolutionService _screenResolutionService;
        private readonly IAssetProvider _assetProvider;
        private readonly GameLoopService _gameLoopService;
        private readonly Camera _camera;
        private readonly StatsStorage _statsStorage;
        private readonly CompositeDisposable _disposables;
        private readonly Wallet _wallet;
        private readonly SignalBus _signalBus;

        private StatsBuilder _statsBuilder;
        
        private readonly Transform _gameParent;
        private readonly Transform _poolsParent;
        private readonly Transform _enemyPoolParent;
        
        public GameFactory(
            IInstantiator instantiator,
            IGameLoadDataService gameLoadDataService,
            IGetTargetPosition getTargetPosition,
            IScreenResolutionService screenResolutionService,
            IAssetProvider assetProvider,
            GameLoopService gameLoopService,
            StatsStorage statsStorage,
            CompositeDisposable disposables,
            Camera camera,
            Wallet wallet,
            SignalBus signalBus)
        {
            _instantiator = instantiator;
            _getTargetPosition = getTargetPosition;
            _gameLoopService = gameLoopService;
            _statsStorage = statsStorage;
            _disposables = disposables;
            _camera = camera;
            _wallet = wallet;
            _signalBus = signalBus;
            _assetProvider = assetProvider;
            _screenResolutionService = screenResolutionService;
            _gameLoadDataService = gameLoadDataService;
            
            _gameParent = CreateParentObject(GameplayObjectParent);
            _poolsParent = CreateParentObject(PoolsObject);
            _enemyPoolParent = CreateParentObject(EnemiesPool);

            _poolsParent.SetParent(_gameParent);
            _enemyPoolParent.SetParent(_poolsParent);
        }

        public TowerFacade TowerFacade { get; private set; }
        public WaveSpawner WaveSpawner { get; private set; }

        public WavePresenter WavePresenter { get; private set; }

        public WaveSpawner CreateSpawner()
        {
            SpawnPositionEnemy spawnPositionEnemy = 
                new SpawnPositionEnemy(_gameLoadDataService.SpawnerConfig.SpawnMargin, _screenResolutionService);
            WaveSpawner waveSpawner = _instantiator.Instantiate<WaveSpawner>();
            WaveView view = TowerFacade.GetComponentInChildren<WaveView>();
            
            WavePresenter = new WavePresenter(view, waveSpawner);
            WaveSpawner = waveSpawner;
            waveSpawner.Init(spawnPositionEnemy,_gameLoadDataService.SpawnerConfig);

            _gameLoopService.AddGameListener(WavePresenter);

            return waveSpawner;
        }

        public void CreateStats()
        {
            _statsBuilder = new StatsBuilder();

            foreach (StatsConfig current in _gameLoadDataService.TowerConfig.Stats)
            {
                switch (current.Type)
                {
                    case StatsType.None:
                        throw new Exception("Invalid Stats Type");
                    case StatsType.Health:
                        break;
                    case StatsType.PassiveHealing:
                        PassiveHealthStats passiveHealthStats = CreateCurrentStats<PassiveHealthStats>(current);
                        _statsStorage.AddStatsList(passiveHealthStats);
                        break;
                    case StatsType.AmountTargets:
                        AmountTargetsStats amountStats = CreateCurrentStats<AmountTargetsStats>(current);
                        _statsStorage.AddStatsList(amountStats);
                        break;
                    case StatsType.Damage:
                        DamageStats damageStats = CreateCurrentStats<DamageStats>(current);
                        _statsStorage.AddStatsList(damageStats);
                        break;
                }
            }
        }

        public void CreateGameplayVolume()
        {
            GameObject gameplayVolume =
                UnityEngine.Object.Instantiate(_assetProvider.LoadAsset(AssetPath.GameplayVolume),_gameParent);
        }

        public void CreateBackground()
        {
            GameObject backPrefab =
                UnityEngine.Object.Instantiate(_assetProvider.LoadAsset(AssetPath.BackgroundPath),_gameParent);

            backPrefab.transform.localScale = new Vector3(
                _screenResolutionService.ScreenWidth + Padding,
                _screenResolutionService.ScreenHeight + Padding,
                1);
        }

        public void CreateBackgroundEffect()
        {
            GameObject effectPrefab =
                UnityEngine.Object.Instantiate(_assetProvider.LoadAsset(AssetPath.BackgroundEffectPath),_gameParent);

            ParticleSystem particleSystem = effectPrefab.GetComponent<ParticleSystem>();

            ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
            shapeModule.scale = new Vector3(_screenResolutionService.ScreenWidth, _screenResolutionService.ScreenHeight,
                1);
        }

        public TowerFacade CreateTower()
        {
            GameObject towerPrefab = _instantiator.InstantiatePrefab(_assetProvider.LoadAsset(AssetPath.TowerPath),_gameParent);

            TowerFacade towerFacade = towerPrefab.GetComponent<TowerFacade>();
            TowerView view = towerFacade.GetComponent<TowerView>();
            HealthView healthView = towerFacade.GetComponentInChildren<HealthView>();
            WalletView walletView = towerFacade.GetComponentInChildren<WalletView>();
            TimeScaleView timeScaleView = towerFacade.GetComponentInChildren<TimeScaleView>();

            EnemyObserver enemyObserver = towerPrefab.GetComponentInChildren<EnemyObserver>();

            AnimationTower animation = new AnimationTower(view, _camera);
            TowerCallbacks callbacks = new TowerCallbacks(animation);

            EnemiesVault enemiesVault = new EnemiesVault(enemyObserver, _disposables);
            TowerShoot shootComponent = new TowerShoot(enemiesVault, view, _statsStorage);

            HealthStats healthStats = new HealthStats(100);
            HealthPresenter healthPresenter = new HealthPresenter(healthView, healthStats, _disposables);
            WalletPresenter walletPresenter = new WalletPresenter(walletView, _wallet, _disposables);
            TimeScalePresenter timeScalePresenter = new TimeScalePresenter(timeScaleView, _signalBus);

            TakeDamageComponent takeDamageComponent = new TakeDamageComponent(healthStats);
            PassiveHealthComponent passiveHealthComponent = new PassiveHealthComponent(healthStats, _statsStorage);
            RecoverComponent recoverComponent = new RecoverComponent(healthStats);

            SpawnBullet spawnBullet = new SpawnBullet(shootComponent, view, _instantiator);

            towerFacade.transform.position = _getTargetPosition.GetPosition();
            towerFacade.Init(takeDamageComponent, callbacks, recoverComponent);
            timeScalePresenter.Subscribe();

            view.gameObject.SetActive(false);

            _gameLoopService.AddInitializable(
                walletPresenter,
                healthPresenter,
                spawnBullet,
                enemiesVault);

            _gameLoopService.AddDisposable(
                walletPresenter,
                healthPresenter,
                spawnBullet,
                enemiesVault,
                timeScalePresenter);

            _gameLoopService.AddTickable(
                shootComponent,
                passiveHealthComponent);

            _gameLoopService.AddGameListener(towerFacade);

            TowerFacade = towerFacade;

            return towerFacade;
        }

        public EnemyFacade CreateEnemy(EnemyType type)
        {
            EnemyConfig config = _gameLoadDataService.GetEnemyConfig(type);
            EnemyFacade enemy = CreateConcreteEnemy(config);
            
            enemy.transform.SetParent(_enemyPoolParent);

            return enemy;
        }

        private EnemyFacade CreateConcreteEnemy(EnemyConfig config)
        {
            GameObject enemyPrefab = _instantiator.InstantiatePrefab(config.EnemyPrefab);

            EnemyFacade facade = enemyPrefab.GetComponent<EnemyFacade>();
            EnemyView view = enemyPrefab.GetComponent<EnemyView>();

            MoveSpeedStats moveSpeedStats = new MoveSpeedStats(config.GetRandomValueMoveSpeed());
            CollisionDamageStats collisionDamageStats =
                new CollisionDamageStats(config.GetRandomValueCollisionDamage());
            RewardSpendStats rewardSpendStats =
                new RewardSpendStats(config.GetRandomMoneyReward(), config.GetRandomMoneySpend());
            HealthStats healthStats = new HealthStats(config.GetRandomValueHealth());

            RotationComponent rotationComponent = new RotationComponent(view, config.GetRandomRotationSpeed());
            GiveDamageComponent giveDamageComponent = new GiveDamageComponent(collisionDamageStats);
            TakeDamageComponent takeDamageComponent = new TakeDamageComponent(healthStats);

            AnimationEnemy animation = new AnimationEnemy(view);
            EnemyCallbacks enemyCallbacks = new EnemyCallbacks(animation, view, _wallet, rewardSpendStats);
            EnemyMovement movement = new EnemyMovement(_getTargetPosition, facade.transform, moveSpeedStats);

            facade.Init(
                takeDamageComponent,
                giveDamageComponent,
                enemyCallbacks);

            _gameLoopService.AddTickable(
                rotationComponent,
                movement);

            return facade;
        }

        private TStats CreateCurrentStats<TStats>(StatsConfig config) where TStats : Stats, new()
        {
            TStats currentStats = _statsBuilder
                .Reset()
                .WithCurrentLevel(config.InitialLevel)
                .WithMaxLevel(config.MaxLevel)
                .WithPrice(config.PriceTables.GetPrice(config.InitialLevel))
                .WithValueStats(config.ValueTables.GetValue(config.InitialLevel))
                .WithViewStats(config.View)
                .WithPriceTables(config.PriceTables)
                .WithValueTables(config.ValueTables)
                .Build<TStats>();

            return currentStats;
        }

        private Transform CreateParentObject(string name)
        {
            GameObject parenGo = new GameObject(name)
            {
                transform =
                {
                    position = Vector3.zero
                }
            };
            
            return parenGo.transform;
        }
    }
}