using System;
using System.Collections.Generic;
using _Project.Meta.StatsLogic.Upgrade;

namespace _Project.Static
{
    public static class DefaultValueStat
    {
        private const float PassiveHealthDefaultValue = 0.001f;
        private const float DamageStatsDefaultValue = 25;
        private const float AmountTargetsDefaultValue = 1;
        
        private static readonly Dictionary<Type, float> _defaultValueStats = new()
        {
            {typeof(PassiveHealthStat),PassiveHealthDefaultValue},
            {typeof(DamageStat),DamageStatsDefaultValue},
            {typeof(AmountTargetsStat),AmountTargetsDefaultValue}
        };

        public static float GetDefaultStatsValue<TStats>() where TStats : Stat
        {
            if (_defaultValueStats.TryGetValue(typeof(TStats), out float value))
                return value;

            throw new NullReferenceException("No stats found for type " + typeof(TStats).Name);
        }
    }
}