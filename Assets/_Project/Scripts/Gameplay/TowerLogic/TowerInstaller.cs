using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.BulletLogic;
using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Gameplay.Observers;
using _Project.Scripts.Gameplay.Stats;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    public class TowerInstaller : MonoInstaller
    {
        [SerializeField] private TowerConfig _config;
        [SerializeField] private Tower _tower;
        [SerializeField] private EnemyObserver _enemyObserver;
        [SerializeField] private TowerView _view;
        [SerializeField] private HealthView _healthView;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(_view)
                .AsSingle();

            Container
                .Bind<TakeDamageComponent>()
                .AsSingle();
            
            Container
                .BindInstance(_enemyObserver)
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