using System;
using System.Collections.Generic;
using _Project.Meta.StatsLogic.Upgrade;
using _Project.Static;
using JetBrains.Annotations;

namespace _Project.Meta.StatsLogic
{
    [UsedImplicitly]
    public class StatsStorage
    {
        public readonly List<Stats> StatsList = new();
        private readonly Dictionary<Type, Stats> _dictionaryStats = new();

        public void AddStatsList(Stats stats)
        {
            StatsList.Add(stats);

            if (!_dictionaryStats.TryAdd(stats.GetType(), stats))
                throw new Exception($"Attempt to add new stats: {stats.GetType().Name}");
        }

        public float GetStatsValue<TStats>() where TStats : Stats
        {
            return _dictionaryStats.TryGetValue(typeof(TStats), out var stats) 
                ? stats.CurrentValue 
                : Constants.GetStatsValue<TStats>();
        }
    }
}