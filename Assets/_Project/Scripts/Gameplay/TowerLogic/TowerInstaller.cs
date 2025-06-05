using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Gameplay.Observers;
using _Project.Scripts.Gameplay.Stats;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    public class TowerInstaller : MonoInstaller
    {
        [SerializeField] private TowerConfig _config;
        [FormerlySerializedAs("_tower")] [SerializeField] private TowerFacade towerFacade;
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