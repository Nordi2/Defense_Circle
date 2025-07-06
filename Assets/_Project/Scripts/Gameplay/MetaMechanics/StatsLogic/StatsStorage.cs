using System;
using System.Collections.Generic;
using _Project.Data.Config;
using _Project.Meta.StatsLogic.Upgrade;
using JetBrains.Annotations;
using UnityEngine;

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

        public TStats GetStats<TStats>() where TStats : Stats
        {
            if (_dictionaryStats.TryGetValue(typeof(TStats), out var stats))
                return (TStats)stats;

            throw new NullReferenceException("No stats found for type " + typeof(TStats).Name);
        }
    }
}