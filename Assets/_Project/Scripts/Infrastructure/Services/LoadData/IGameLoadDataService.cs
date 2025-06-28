using _Project.Cor.Enemy;
using _Project.Data.Config;

namespace Infrastructure.Services.Services.LoadData
{
    public interface IGameLoadDataService
    {
        void LoadData();
        EnemyConfig GetEnemyConfig(EnemyType type);
    }
}