using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Gameplay.Stats;
using _Project.Scripts.Gameplay.Stats.EnemyStats;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.EnemyLogic
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private EnemyConfig _config;

        public override void InstallBindings()
        {
            Container
                .BindInstance(_config)
                .AsSingle();

            Container
                .Bind<ShowStats>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<EnemyMovement>()
                .AsSingle()
                .WithArguments(_enemy.transform);

            Container
                .Bind<GiveDamageComponent>()
                .AsSingle();
            
            Container
                .Bind<TakeDamageComponent>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<HealthStat>()
                .AsSingle()
                .WithArguments(_config.GetRandomValueHealth());

            Container
                .BindInterfacesAndSelfTo<MoveSpeedStat>()
                .AsSingle()
                .WithArguments(_config.GetRandomValueMoveSpeed());

            Container
                .BindInterfacesAndSelfTo<CollisionDamageStat>()
                .AsSingle()
                .WithArguments(_config.GetRandomValueCollisionDamage());
        }
    }
}