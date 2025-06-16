using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.StatsLogic.TowerStats;

namespace _Project.Scripts.Gameplay
{
    public static class Constants
    {
        private const int DEFAULT_DAMAGE_VALUE_STATS = 25;
        private const int DEFAULT_COUNT_TARGETS_VALUE_STATS = 1;
        private const float DEFAULT_RELOAD_VALUE_STATS = 2.5F;

        private static readonly Dictionary<Type, float> _dictionaryValuesDefaultStats = new()
        {
            { typeof(CountTargetStats), DEFAULT_COUNT_TARGETS_VALUE_STATS }
        };

        public static float GetDefaultValueStats<T>() where T : StatsLogic.Stats =>
            _dictionaryValuesDefaultStats[typeof(T)];
    }
}