using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.StatsLogic.TowerStats;
using JetBrains.Annotations;
using Unity.Burst.CompilerServices;

namespace _Project.Scripts.Gameplay.StatsLogic
{
    [UsedImplicitly]
    public class StatsStorage : IDisposable
    {
        public IReadOnlyList<Stats> ListStats;

        private readonly Dictionary<Type, float> _dictionaryStats = new();

        public int CountTargetsValue => (int)GetValue<CountTargetStats>();


        void IDisposable.Dispose()
        {
            foreach (Stats stat in ListStats)
            {
                stat.OnUpgradeStats -= SwitchValueStats;
            }
        }

        public void AddStatsInStorage(List<Stats> stats)
        {
            ListStats = new List<Stats>(stats);

            foreach (Stats current in ListStats)
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
}