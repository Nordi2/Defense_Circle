using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.BulletLogic;
using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Gameplay.Observers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    public class TowerInstaller : MonoInstaller
    {
        [SerializeField] private TowerConfig _config;
        [SerializeField] private Tower _tower;
        [SerializeField] private EnemyObserver _enemyObserver;
        [SerializeField] private SpriteRenderer[] _sprites;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private TowerView _view;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(_enemyObserver)
                .AsSingle();
            
            Container
                .Bind<HealthComponent>()
                .AsSingle()
                .WithArguments(_config.MaxHealth);

            Container
                .BindInterfacesAndSelfTo<EnemysVault>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<AnimationTower>()
                .AsSingle()
                .WithArguments(_sprites);

            Container
                .BindInterfacesAndSelfTo<TowerShoot>()
                .AsSingle()
                .WithArguments(_shootPoint);

            Container
                .BindInterfacesTo<SpawnBullet>()
                .AsSingle()
                .WithArguments(_bulletPrefab)
                .NonLazy();
        }
    }
}