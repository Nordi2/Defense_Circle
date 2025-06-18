/*using System.Collections.Generic;
using _Project.Data.Config;
using _Project.Data.Stats.Config;
using _Project.Data.Stats.Config.Parent;
using _Project.Data.Tables.Parent;
using _Project.Meta.Stats;
using _Project.Meta.Stats.Upgrade;
using Infrastructure.Services;
using JetBrains.Annotations;
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
            List<Stats> stats = new List<Stats>();

            foreach (StatsConfig config in _towerConfig.TowerStats)
            {
                switch (config)
                {
                    case CountTargetStatsConfig countTargetStats:
                        stats.Add(CreateCurrentStats<CountTargetStats>(countTargetStats,countTargetStats.CountTargetsTables));
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
}*/