using System;
using System.Collections.Generic;
using _Project.Meta.StatsLogic.Upgrade;

namespace _Project.Static
{
    public static class Constants
    {
        private const float PassiveHealthDefaultValue = 0.001f;
        private const float DamageStatsDefaultValue = 25;
        private const float AmountTargetsDefaultValue = 1;
        
        private static readonly Dictionary<Type, float> _defaultValueStats = new()
        {
            {typeof(PassiveHealthStats),PassiveHealthDefaultValue},
            {typeof(DamageStats),DamageStatsDefaultValue},
            {typeof(AmountTargetsStats),AmountTargetsDefaultValue}
        };

        public static float GetStatsValue<TStats>() where TStats : Stats
        {
            if (_defaultValueStats.TryGetValue(typeof(TStats), out float value))
                return value;

            throw new NullReferenceException("No stats found for type " + typeof(TStats).Name);
        }
    }
}