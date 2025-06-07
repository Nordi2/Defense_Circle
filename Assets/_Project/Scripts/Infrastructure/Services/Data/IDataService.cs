using _Project.Scripts.Data;

namespace _Project.Scripts.Infrastructure.Services.Data
{
    public interface IDataService
    {
        TowerConfig GetTowerConfig();
        EnemyConfig GetEnemyConfig();
        void LoadData();
    }
}