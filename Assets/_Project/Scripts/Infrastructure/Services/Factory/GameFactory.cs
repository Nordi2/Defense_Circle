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
        private readonly StatStorage _statStorage;
        private readonly CompositeDisposable _disposables;
        private readonly Wallet _wallet;
        private readonly SignalBus _signalBus;

        private StatBuilder _statBuilder;

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
            StatStorage statStorage,
            CompositeDisposable disposables,
            Camera camera,
            Wallet wallet,
            SignalBus signalBus)
        {
            _instantiator = instantiator;
            _getTargetPosition = getTargetPosition;
            _gameLoopService = gameLoopService;
            _statStorage = statStorage;
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
            var spawnPositionEnemy = new SpawnPositionEnemy(_gameLoadDataService.SpawnerConfig.SpawnMargin, _screenResolutionService);
            var waveSpawner = _instantiator.Instantiate<WaveSpawner>();
            var view = TowerFacade.GetComponentInChildren<WaveView>();

            WavePresenter = new WavePresenter(view, waveSpawner);
            WaveSpawner = waveSpawner;
            waveSpawner.Init(spawnPositionEnemy, _gameLoadDataService.SpawnerConfig);

            _gameLoopService.AddGameListener(WavePresenter);

            return waveSpawner;
        }

        public void CreateStats()
        {
            _statBuilder = new StatBuilder();

            foreach (StatsConfig current in _gameLoadDataService.TowerConfig.Stats)
            {
                switch (current.Type)
                {
                    case StatType.None:
                        throw new Exception("Invalid Stats Type");

                    case StatType.PassiveHealing:
                        PassiveHealthStat passiveHealthStat = CreateCurrentStats<PassiveHealthStat>(current);
                        _statStorage.AddStatsList(passiveHealthStat);
                        break;

                    case StatType.AmountTargets:
                        AmountTargetsStat amountStat = CreateCurrentStats<AmountTargetsStat>(current);
                        _statStorage.AddStatsList(amountStat);
                        break;

                    case StatType.Damage:
                        DamageStat damageStat = CreateCurrentStats<DamageStat>(current);
                        _statStorage.AddStatsList(damageStat);
                        break;
                }
            }
        }

        public void CreateGameplayVolume()
        {
            UnityEngine.Object.Instantiate(_assetProvider.LoadAsset(AssetPath.GameplayVolume), _gameParent);
        }

        public void CreateBackground()
        {
            GameObject backPrefab =
                UnityEngine.Object.Instantiate(_assetProvider.LoadAsset(AssetPath.BackgroundPath), _gameParent);

            backPrefab.transform.localScale = new Vector3(
                _screenResolutionService.ScreenWidth + Padding,
                _screenResolutionService.ScreenHeight + Padding,
                1);
        }

        public void CreateBackgroundEffect()
        {
            GameObject effectPrefab =
                UnityEngine.Object.Instantiate(_assetProvider.LoadAsset(AssetPath.BackgroundEffectPath), _gameParent);

            var particleSystem = effectPrefab.GetComponent<ParticleSystem>();

            ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
            shapeModule.scale = new Vector3(_screenResolutionService.ScreenWidth, _screenResolutionService.ScreenHeight,
                1);
        }

        public TowerFacade CreateTower()
        {
            GameObject towerPrefab =
                _instantiator.InstantiatePrefab(_assetProvider.LoadAsset(AssetPath.TowerPath), _gameParent);

            var towerFacade = towerPrefab.GetComponent<TowerFacade>();
            var view = towerFacade.GetComponent<TowerView>();
            var healthView = towerFacade.GetComponentInChildren<HealthView>();
            var walletView = towerFacade.GetComponentInChildren<WalletView>();
            var timeScaleView = towerFacade.GetComponentInChildren<TimeScaleView>();
            var enemyObserver = towerPrefab.GetComponentInChildren<EnemyObserver>();

            var animation = new AnimationTower(view, _camera);
            var callbacks = new TowerCallbacks(animation);

            var enemiesVault = new EnemiesInCircle(enemyObserver, _disposables);
            var shootComponent = new TowerShoot(enemiesVault, view, _statStorage);

            var healthStat = new HealthStat(100);
            var healthPresenter = new HealthPresenter(healthView, healthStat, _disposables);
            var walletPresenter = new WalletPresenter(walletView, _wallet, _disposables);
            var timeScalePresenter = new TimeScalePresenter(timeScaleView, _signalBus);

            var takeDamageComponent = new TakeDamageComponent(healthStat);
            var passiveHealthComponent = new PassiveHealthComponent(healthStat, _statStorage);
            var recoverComponent = new RecoverComponent(healthStat);

            var spawnBullet = new SpawnBullet(shootComponent, view, _instantiator);

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

            var facade = enemyPrefab.GetComponent<EnemyFacade>();
            var view = enemyPrefab.GetComponent<EnemyView>();

            var moveSpeedStats = new MoveSpeedStat(config.GetRandomValueMoveSpeed());
            var collisionDamageStats = new CollisionDamageStat(config.GetRandomValueCollisionDamage());
            var rewardSpendStats = new RewardSpendStat(config.GetRandomMoneyReward(), config.GetRandomMoneySpend());
            var healthStat = new HealthStat(config.GetRandomValueHealth());

            var rotationComponent = new RotationComponent(view, config.GetRandomRotationSpeed());
            var giveDamageComponent = new GiveDamageComponent(collisionDamageStats);
            var takeDamageComponent = new TakeDamageComponent(healthStat);

            var animation = new AnimationEnemy(view);
            var enemyCallbacks = new EnemyCallbacks(animation, view, _wallet, rewardSpendStats);
            var movement = new EnemyMovement(_getTargetPosition, facade.transform, moveSpeedStats);

            facade.Init(
                takeDamageComponent,
                giveDamageComponent,
                enemyCallbacks);

            _gameLoopService.AddTickable(
                rotationComponent,
                movement);

            return facade;
        }

        private TStats CreateCurrentStats<TStats>(StatsConfig config) where TStats : Stat, new()
        {
            TStats currentStats = _statBuilder
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