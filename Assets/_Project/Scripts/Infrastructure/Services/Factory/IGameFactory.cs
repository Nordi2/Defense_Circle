using _Project.Scripts.Gameplay.Tower;

namespace _Project.Scripts.Infrastructure.Services.Factory
{
    public interface IGameFactory
    {
        TowerFacade CreateTower();
    }
}