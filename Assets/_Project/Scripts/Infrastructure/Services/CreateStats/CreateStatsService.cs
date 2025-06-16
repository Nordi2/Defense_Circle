using System.Collections.Generic;
using Data.Config;
using Data.Stats.Config;
using Data.Stats.Config.Parent;
using Data.Tables.Parent;
using Infrastructure.Services;
using JetBrains.Annotations;
using Meta.Stats;
using Meta.Stats.Upgrade;
using UnityEngine;

namespace _Project.Infrastructure.Services
{
    [UsedImplicitly]
    public class CreateStatsService :
        ICreateStatsService
    {
        private readonly StatsBuilder _statsBuilder;
        private readonly TowerConfig _towerConfig;

        public CreateStatsService(
            StatsBuilder statsBuilder,
            TowerConfig towerConfig)
        {
            _statsBuilder = statsBuilder;
            _towerConfig = towerConfig;
        }

        public List<Stats> CreateStats()
        {
            var stats = new List<Stats>();

            foreach (StatsConfig config in _towerConfig.TowerStats)
            {
                switch (config)
                {
                    case CountTargetStatsConfig countTargetStats:
                        stats.Add(CreateCurrentStats<CountTargetStats>(countTargetStats,countTargetStats.CountTargetsTables));
                        Debug.Log("Create stats");
                        break;
                }
            }
            
            return stats;
        }

        private TStats CreateCurrentStats<TStats>(StatsConfig config, StatsTables statsTables) where TStats : Stats,new()
        {
            TStats currentStats = _statsBuilder
                .Reset()
                .WithCurrentLevel(config.InitialLevel)
                .WithMaxLevel(config.MaxLevel)
                .WithIcon(config.Icon)
                .WithPrice(config.PriceTable.GetValue(config.InitialLevel))
                .WithPriceTable(config.PriceTable)
                .WithStatsTable(statsTables)
                .WithValueStats(statsTables.GetValue(config.InitialLevel))
                .Build<TStats>();
            
            return currentStats;
        }
    }
}