using System;
using System.Collections.Generic;
using _Project.Meta.StatsLogic.Upgrade;
using _Project.Static;
using JetBrains.Annotations;

namespace _Project.Meta.StatsLogic
{
    [UsedImplicitly]
    public class StatStorage
    {
        public readonly List<Stat> StatsList = new();
        
        private readonly Dictionary<Type, Stat> _dictionaryStats = new();

        public void AddStatsList(Stat stat)
        {
            StatsList.Add(stat);

            if (!_dictionaryStats.TryAdd(stat.GetType(), stat))
                throw new Exception($"Attempt to add new stats: {stat.GetType().Name}");
        }

        public float GetStatsValue<TStats>() where TStats : Stat
        {
            return _dictionaryStats.TryGetValue(typeof(TStats), out var stats) 
                ? stats.CurrentValue 
                : DefaultValueStat.GetDefaultStatsValue<TStats>();
        }
    }
}