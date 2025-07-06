using _Project.Cor.Enemy;
using _Project.Cor.Enemy.Mono;
using _Project.Cor.Spawner;
using _Project.Cor.Tower.Mono;

namespace _Project.Infrastructure.Services
{
    public interface IGameFactory
    {
        TowerFacade CreateTower();
        EnemyFacade CreateEnemy(EnemyType type);
        void CreateBackground();
        void CreateBackgroundEffect();
        void CreateGameplayVolume();
        void CreateStats();
        WaveSpawner CreateSpawner();
    }
}