using _Project.Scripts.Data;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.Services.Data
{
    [UsedImplicitly]
    public class DataService :
        IDataService
    {
        private const string EnemyConfigPath = "Data/EnemyConfig";

        private TowerConfig _towerConfig;
        private EnemyConfig _enemyConfig;

        public void LoadData()
        {
            _towerConfig = Resources.Load<TowerConfig>("Data/TowerConfig");
            _enemyConfig = Resources.Load<EnemyConfig>(EnemyConfigPath);
        }

        public EnemyConfig GetEnemyConfig() => 
            _enemyConfig;

        public TowerConfig GetTowerConfig() =>
            _towerConfig;
    }
}