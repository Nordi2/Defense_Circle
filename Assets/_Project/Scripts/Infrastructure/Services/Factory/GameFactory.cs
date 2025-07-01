using _Project.Cor;
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
using _Project.Static;
using DebugToolsPlus;
using Infrastructure.Services;
using Infrastructure.Services.Services.LoadData;
using System;
using Infrastructure.Services.Services.ScreenResolution;
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
        private readonly IInstantiator _instantiator;
        private readonly IGetTargetPosition _getTargetPosition;
        private readonly IGameLoadDataService _gameLoadDataService;
        private readonly GameLoopService _gameLoopService;
        private readonly Camera _camera;
        private readonly StatsStorage _statsStorage;
        private readonly CompositeDisposable _disposables;
        private readonly ShowStatsService _showStatsService;
        private readonly Wallet _wallet;
        private readonly IScreenResolutionService _screenResolutionService;
        
        public TowerFacade TowerFacade { get; private set; }

        public GameFactory(
            IInstantiator instantiator,
            IGameLoadDataService gameLoadDataService,
            IGetTargetPosition getTargetPosition,
            GameLoopService gameLoopService,
            StatsStorage statsStorage,
            CompositeDisposable disposables,
            ShowStatsService showStatsService,
            Camera camera,
            Wallet wallet,
            IScreenResolutionService screenResolutionService)
        {
            _instantiator = instantiator;
            _getTargetPosition = getTargetPosition;
            _gameLoopService = gameLoopService;
            _statsStorage = statsStorage;
            _disposables = disposables;
            _showStatsService = showStatsService;
            _camera = camera;
            _wallet = wallet;
            _screenResolutionService = screenResolutionService;
            _gameLoadDataService = gameLoadDataService;
        }

        public void CreateBackground()
        {
            GameObject backPrefab = UnityEngine.Object.Instantiate(Resources.Load<GameObject>(AssetPath.BackgroundPath));
            
            backPrefab.transform.localScale = new Vector3(
                _screenResolutionService.ScreenWidth,
                _screenResolutionService.ScreenHeight,
                1);
        }

        public void CreateBackgroundEffect()
        {
            GameObject effectPrefab = UnityEngine.Object.Instantiate(Resources.Load<GameObject>(AssetPath.BackgroundEffectPath));
            
            var particleSystem = effectPrefab.GetComponent<ParticleSystem>();

            var shapeModule = particleSystem.shape;
            shapeModule.scale = new Vector3(_screenResolutionService.ScreenWidth, _screenResolutionService.ScreenHeight, 1);
        }

        public TowerFacade CreateTower()
        {
            D.Log(GetType().Name, "Create TOWER", DColor.GREEN, true);

            GameObject towerPrefab = _instantiator.InstantiatePrefab(Resources.Load(AssetPath.TowerPath));

            TowerFacade towerFacade = towerPrefab.GetComponent<TowerFacade>();
            TowerView view = towerFacade.GetComponent<TowerView>();
            HealthView healthView = towerFacade.GetComponentInChildren<HealthView>();
            WalletView walletView = towerFacade.GetComponentInChildren<WalletView>();

            EnemyObserver enemyObserver = towerPrefab.GetComponentInChildren<EnemyObserver>();

            AnimationTower animation = new AnimationTower(view, _camera);
            TowerCallbacks callbacks = new TowerCallbacks(animation);

            EnemysVault enemysVault = new EnemysVault(enemyObserver, _disposables);
            TowerShoot shootComponent = new TowerShoot(enemysVault, view, _statsStorage);

            HealthStats healthStats = new HealthStats(100);
            HealthPresenter healthPresenter = new HealthPresenter(healthView, healthStats, _disposables);
            WalletPresenter walletPresenter = new WalletPresenter(walletView, _wallet, _disposables);

            TakeDamageComponent takeDamageComponent = new TakeDamageComponent(healthStats);
            PassiveHealthComponent passiveHealthComponent = new PassiveHealthComponent(healthStats, _statsStorage);
            RecoverComponent recoverComponent = new RecoverComponent(healthStats);

            SpawnBullet spawnBullet = new SpawnBullet(shootComponent, view, _instantiator);

            towerFacade.transform.position = _getTargetPosition.GetPosition();
            towerFacade.Init(takeDamageComponent, callbacks, _showStatsService, recoverComponent);

            view.gameObject.SetActive(false);

            _gameLoopService.AddInitializable(
                walletPresenter,
                healthPresenter,
                spawnBullet,
                enemysVault);

            _gameLoopService.AddDisposable(
                walletPresenter,
                healthPresenter,
                spawnBullet,
                enemysVault);

            _gameLoopService.AddTickable(
                shootComponent,
                passiveHealthComponent);

            _gameLoopService.AddGameListener(towerFacade);

            TowerFacade = towerFacade;

            return towerFacade;
        }

        public EnemyFacade CreateEnemy(EnemyType type)
        {
            return type switch
            {
                EnemyType.Default => LoadConfigAndCreateEnemy(EnemyType.Default),
                EnemyType.Fast => LoadConfigAndCreateEnemy(EnemyType.Fast),
                EnemyType.Slow => LoadConfigAndCreateEnemy(EnemyType.Slow),
                _ => throw new NotImplementedException()
            };
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

        private EnemyFacade LoadConfigAndCreateEnemy(EnemyType type)
        {
            D.Log(GetType().Name, $"Create Enemy {type}", DColor.GREEN, true);

            EnemyConfig enemyConfig = _gameLoadDataService.GetEnemyConfig(type);
            return CreateConcreteEnemy(enemyConfig);
        }
    }
}