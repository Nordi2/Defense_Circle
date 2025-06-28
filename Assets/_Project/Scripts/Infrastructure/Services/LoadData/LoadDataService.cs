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

        public void LoadData()
        {
            EnemyConfig[] enemyConfig = Resources.LoadAll<EnemyConfig>("Data/Enemys");

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