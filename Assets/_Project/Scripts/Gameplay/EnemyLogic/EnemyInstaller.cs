using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Component;
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
                .Bind<EnemyStats>()
                .FromMethod(CreateEnemyStats)
                .AsSingle();

            Container
                .BindInstance(_config)
                .AsSingle();

            Container
                .BindInterfacesTo<EnemyMovement>()
                .AsSingle()
                .WithArguments(_enemy.transform);

            Container
                .Bind<HealthComponent>()
                .AsSingle();
        }

        private EnemyStats CreateEnemyStats()
        {
            EnemyStats enemyStats = new EnemyStats(
                _config.GetRandomValueHealth(),
                _config.GetRandomValueMoveSpeed(),
                _config.GetRandomValueCollisionDamage());
            
            return enemyStats;
        }
    }
}