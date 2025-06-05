using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Gameplay.Observers;
using _Project.Scripts.Gameplay.Stats;
using _Project.Scripts.Gameplay.Tower.Animation;
using _Project.Scripts.Gameplay.Tower.Callbacks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Tower
{
    public class TowerInstaller : MonoInstaller
    {
        [SerializeField] private TowerConfig _config;
        [SerializeField] private EnemyObserver _enemyObserver;
        [SerializeField] private TowerView _view;
        [SerializeField] private HealthView _healthView;

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
        }
    }
}