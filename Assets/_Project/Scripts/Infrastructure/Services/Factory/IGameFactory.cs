using Cor.Tower.Mono;

namespace _Project.Infrastructure.Services
{
    public interface IGameFactory
    {
        TowerFacade CreateTower();
    }
}