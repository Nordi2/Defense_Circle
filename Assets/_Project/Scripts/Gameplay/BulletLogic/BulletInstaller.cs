using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Gameplay.EnemyLogic;
using _Project.Scripts.Gameplay.Stats.EnemyStats;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.BulletLogic
{
    public class BulletInstaller : MonoInstaller
    {
        [SerializeField] private Transform _bulletTransform;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private BulletConfig _config;
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<BulletMovement>()
                .AsSingle()
                .WithArguments(_bulletTransform);

            Container
                .BindInterfacesAndSelfTo<MoveSpeedStat>()
                .AsSingle()
                .WithArguments(_config.MoveSpeed);
            
            Container
                .BindInterfacesAndSelfTo<CollisionDamageStat>()
                .AsSingle()
                .WithArguments(_config.GetRandomDamage());

            Container
                .Bind<GiveDamageComponent>()
                .AsSingle();

            Container.Bind<ShowStats>().AsSingle();
        }
    }
}