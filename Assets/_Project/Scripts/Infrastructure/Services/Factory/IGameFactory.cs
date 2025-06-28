
using System;
using _Project.Cor.Enemy;
using _Project.Cor.Enemy.Mono;
using _Project.Cor.Tower.Mono;

namespace _Project.Infrastructure.Services
{
    public interface IGameFactory
    {
        TowerFacade CreateTower();
        EnemyFacade CreateEnemy(EnemyType type);
    }
}