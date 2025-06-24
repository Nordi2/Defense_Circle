using _Project.Cor;
using _Project.Cor.Component;
using _Project.Cor.Enemy;
using _Project.Cor.Enemy.Mono;
using _Project.Cor.Interfaces;
using _Project.Cor.Observers;
using _Project.Cor.Tower;
using _Project.Cor.Tower.Animation;
using _Project.Cor.Tower.Mono;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using _Project.Meta.StatsLogic.NoneUpgrade;
using _Project.Meta.StatsLogic.Upgrade;
using _Project.Scripts.Gameplay.Component;
using _Project.Static;
using DebugToolsPlus;
using Infrastructure.Services;
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
        private readonly GameLoopService _gameLoopService;
        private readonly Camera _camera;
        private readonly StatsStorage _statsStorage;
        private readonly CompositeDisposable _disposables;
        private readonly ShowStatsService _showStatsService;
        private readonly Wallet _wallet;

        public GameFactory(
            IInstantiator instantiator,
            IGetTargetPosition getTargetPosition,
            GameLoopService gameLoopService,
            StatsStorage statsStorage,
            CompositeDisposable disposables,
            ShowStatsService showStatsService,
            Camera camera,
            Wallet wallet)
        {
            _instantiator = instantiator;
            _getTargetPosition = getTargetPosition;
            _gameLoopService = gameLoopService;
            _statsStorage = statsStorage;
            _disposables = disposables;
            _showStatsService = showStatsService;
            _camera = camera;
            _wallet = wallet;
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

            SpawnBullet spawnBullet = new SpawnBullet(shootComponent, view, _instantiator);

            towerFacade.transform.position = _getTargetPosition.GetPosition();
            towerFacade.Init(takeDamageComponent, callbacks, _showStatsService);

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

            return towerFacade;
        }

        public EnemyFacade CreateEnemy()
        {
            GameObject enemyPrefab = _instantiator.InstantiatePrefab(Resources.Load(AssetPath.EnemyDefaultPath));

            EnemyFacade facade = enemyPrefab.GetComponent<EnemyFacade>();
            EnemyView view = enemyPrefab.GetComponent<EnemyView>();

            MoveSpeedStats moveSpeedStatsStats = new MoveSpeedStats(25);
            CollisionDamageStats collisionDamageStatsStats = new CollisionDamageStats(25);
            RewardSpendStats rewardSpendStats = new RewardSpendStats(25, 25);
            HealthStats healthStats = new HealthStats(100);

            AnimationEnemy animation = new AnimationEnemy(view);
            RotationComponent rotationComponent = new RotationComponent(view, 20f);
            GiveDamageComponent giveDamageComponent = new GiveDamageComponent(collisionDamageStatsStats);
            TakeDamageComponent takeDamageComponent = new TakeDamageComponent(healthStats);

            EnemyCallbacks enemyCallbacks = new EnemyCallbacks(animation, view, _wallet, rewardSpendStats);
            EnemyMovement movement = new EnemyMovement(_getTargetPosition, facade.transform, moveSpeedStatsStats);

            facade.Init(
                takeDamageComponent,
                giveDamageComponent,
                enemyCallbacks);

            _gameLoopService.AddTickable(
                rotationComponent,
                movement);
            
            return facade;
        }
    }
}