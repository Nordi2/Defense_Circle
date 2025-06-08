using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Gameplay.Observers;
using _Project.Scripts.Gameplay.Stats;
using _Project.Scripts.Gameplay.Tower.Animation;
using _Project.Scripts.Gameplay.Tower.Callbacks;
using _Project.Scripts.UI.Presenters;
using _Project.Scripts.UI.View;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Tower
{
    public class TowerInstaller : MonoInstaller
    {
        [SerializeField] private EnemyObserver _enemyObserver;
        [SerializeField] private TowerView _view;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private TowerConfig _config;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(_view)
                .AsSingle();

            Container
                .BindInstance(_enemyObserver)
                .AsSingle();

            Container
                .Bind<TowerCallbacks>()
                .AsSingle();

            Container
                .Bind<TakeDamageComponent>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<EnemysVault>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<AnimationTower>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<TowerShoot>()
                .AsSingle();

            Container
                .BindInterfacesTo<SpawnBullet>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<HealthStat>()
                .AsSingle()
                .WithArguments(_config.MaxHealth);

            Container
                .Bind<HealthView>()
                .FromInstance(_healthView)
                .AsSingle();

            Container
                .BindInterfacesTo<HealthPresenter>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<WalletPresenter>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<WalletView>()
                .FromInstance(_walletView)
                .AsSingle();
        }
    }
}