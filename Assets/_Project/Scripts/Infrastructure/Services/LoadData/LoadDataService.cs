using System.Collections.Generic;
using _Project.Cor.Enemy;
using _Project.Data.Config;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Services.Services.LoadData
{
    [UsedImplicitly]
    public class LoadDataService :
        IGameLoadDataService
    {
        private Dictionary<EnemyType, EnemyConfig> _enemyConfigs;

        public TowerConfig TowerConfig { get; private set; }
        public SpawnerConfig SpawnerConfig { get; private set; }
        
        public void LoadData()
        {
            TowerConfig = Resources.Load<TowerConfig>("Data/TowerConfig");
            SpawnerConfig = Resources.Load<SpawnerConfig>("Data/SpawnerConfig");
            EnemyConfig[] enemyConfig = Resources.LoadAll<EnemyConfig>("Data/Enemies");

            _enemyConfigs = new Dictionary<EnemyType, EnemyConfig>(enemyConfig.Length);

            foreach (EnemyConfig config in enemyConfig)
                _enemyConfigs.Add(config.Type, config);
        }

        public EnemyConfig GetEnemyConfig(EnemyType type)
        {
            if (_enemyConfigs.TryGetValue(type, out EnemyConfig config))
                return config;

            throw new KeyNotFoundException();
        }
    }
}