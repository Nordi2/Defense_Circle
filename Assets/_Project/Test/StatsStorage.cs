/*using System;
using System.Collections.Generic;
using _Project.Meta.Stats.Upgrade;
using _Project.Static;
using JetBrains.Annotations;

namespace _Project.Meta.Stats
{
    [UsedImplicitly]
    public class StatsStorage : 
        IDisposable
    {
        private IReadOnlyList<Stats> _listStats;

        private readonly Dictionary<Type, float> _dictionaryStats = new();

        public int CountTargetsValue => (int)GetValue<CountTargetStats>();


        void IDisposable.Dispose()
        {
            foreach (Stats stat in _listStats)
            {
                stat.OnUpgradeStats -= SwitchValueStats;
            }
        }

        public void AddStatsInStorage(List<Stats> stats)
        {
            _listStats = new List<Stats>(stats);

            foreach (Stats current in _listStats)
            {
                current.OnUpgradeStats += SwitchValueStats;
                _dictionaryStats.Add(current.GetType(), current.ValueStats);
            }
        }

        private float GetValue<T>() where T : Stats
        {
            return _dictionaryStats.ContainsKey(typeof(T))
                ? _dictionaryStats[typeof(T)]
                : Constants.GetDefaultValueStats<T>();
        }

        private void SwitchValueStats(Stats stats)
        {
            if (_dictionaryStats.ContainsKey(stats.GetType()))
                _dictionaryStats[stats.GetType()] = stats.ValueStats;
        }
    }
}*/