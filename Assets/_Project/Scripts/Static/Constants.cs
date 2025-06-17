using System;
using System.Collections.Generic;
using _Project.Meta.Stats;
using _Project.Meta.Stats.Upgrade;

namespace _Project.Static
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

        public static float GetDefaultValueStats<T>() where T : Stats =>
            _dictionaryValuesDefaultStats[typeof(T)];
    }
}