using System;
using _Project.Cor;
using _Project.Data.Config;
using _Project.Meta.StatsLogic.Upgrade;
using JetBrains.Annotations;

namespace _Project.Meta.StatsLogic
{
    [UsedImplicitly]
    public class CreateStatsService : ICreateStatsService
    {
        private readonly TowerConfig _config;
        private readonly StatsBuilder _statsBuilder;
        private readonly StatsStorage _statsStorage;
        private readonly ShowStatsService _showStatsService;
        
        public CreateStatsService(
            TowerConfig config,
            StatsBuilder statsBuilder,
            StatsStorage statsStorage,
            ShowStatsService showStatsService)
        {
            _config = config;
            _statsBuilder = statsBuilder;
            _statsStorage = statsStorage;
            _showStatsService = showStatsService;
        }
        
        public void CreateStats()
        {
            foreach (StatsConfig current in _config.Stats)
            {
                switch (current.Type)
                {
                    case StatsType.None:
                        throw new Exception("Invalid Stats Type");
                    case StatsType.Health:
                        break;
                    case StatsType.AmountTargets:
                        AmountTargetsStats stats = CreateCurrentStats<AmountTargetsStats>(current);
                        _statsStorage.AddStatsList(stats);
                        _showStatsService.AddStats(stats);
                        break;
                }
            }
        }

        private TStats CreateCurrentStats<TStats>(StatsConfig config) where TStats : Stats, new()
        {
            TStats currentStats = _statsBuilder
                .Reset()
                .WithCurrentLevel(config.InitialLevel)
                .WithMaxLevel(config.MaxLevel)
                .WithPrice(config.GetPrice(config.InitialLevel))
                .WithValueStats(config.GetValue(config.InitialLevel))
                .WithViewStats(config.View)
                .Build<TStats>();

            return currentStats;
        }
    }
}