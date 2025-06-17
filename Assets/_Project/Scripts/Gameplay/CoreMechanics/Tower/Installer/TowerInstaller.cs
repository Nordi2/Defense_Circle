using _Project.Cor.Component;
using _Project.Cor.Observers;
using _Project.Cor.Tower.Animation;
using _Project.Cor.Tower.Mono;
using _Project.Data.Config;
using _Project.Meta.Money;
using _Project.Meta.Stats.Upgrade;
using _Project.Scripts.Gameplay.Component;
using UnityEngine;
using Zenject;

namespace _Project.Cor.Tower.Installer
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
                .Bind<HealthStats>()
                .AsSingle()
                .WithArguments(100);

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