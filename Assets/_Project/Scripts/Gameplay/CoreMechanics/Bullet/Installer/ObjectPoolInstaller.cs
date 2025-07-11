using _Project.Cor.Enemy;
using _Project.Cor.Enemy.Mono;
using _Project.Data.Config;
using _Project.Infrastructure.Services;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace _Project.Cor.BulletLogic.Installer
{
    public class ObjectPoolInstaller : MonoInstaller
    {
        [SerializeField] private ObjectPoolConfig _objectPoolConfig;
        
        private IGameFactory _gameFactory;

        [Inject]
        private void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public override void InstallBindings()
        {
            Container
                .BindMemoryPool<EnemyFacade, EnemyPool>()
                .WithId(EnemyType.Default)
                .WithInitialSize(_objectPoolConfig.PoolSizeEnemyDefault)
                .ExpandByOneAtATime()
                .FromMethod(_ => _gameFactory.CreateEnemy(EnemyType.Default))
                .NonLazy();
            
            Container
                .BindMemoryPool<EnemyFacade, EnemyPool>()
                .WithId(EnemyType.Fast)
                .WithInitialSize(_objectPoolConfig.PoolSizeEnemySlow)
                .ExpandByOneAtATime()
                .FromMethod(_ => _gameFactory.CreateEnemy(EnemyType.Slow))
                .NonLazy();

            Container
                .BindMemoryPool<EnemyFacade, EnemyPool>()
                .WithId(EnemyType.Fast)
                .WithInitialSize(_objectPoolConfig.PoolSizeEnemyFast)
                .ExpandByOneAtATime()
                .FromMethod(_ => _gameFactory.CreateEnemy(EnemyType.Fast))
                .NonLazy();
        }
    }
}