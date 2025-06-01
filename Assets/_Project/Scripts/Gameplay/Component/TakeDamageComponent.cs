using System;
using _Project.Scripts.Gameplay.Stats;
using DebugToolsPlus;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Component
{
    [UsedImplicitly]
    public class TakeDamageComponent
    {
        private readonly HealthStat _healthStat;

        public TakeDamageComponent(HealthStat healthStat)
        {
            _healthStat = healthStat;
        }

        public void TakeDamage(
            int damage,
            out bool isDie,
            Action<int> takeDamageCallback = null,
            Type type = null,
            GameObject contextInfo = null)
        {
            int newValue = Math.Clamp(
                _healthStat.CurrentHealth.CurrentValue - damage,
                0,
                _healthStat.MaxHealth.CurrentValue);

            _healthStat.SetCurrentHealthValue(newValue);

            isDie = newValue <= 0;

            if (!isDie)
                takeDamageCallback?.Invoke(damage);

            D.Log(GetType().Name,
                message: $"{type?.Name}\n" +
                         $"TakeDamage : {damage}," +
                         $"MaxHealth: {_healthStat.MaxHealth.CurrentValue}," +
                         $"OldHealth: {_healthStat.CurrentHealth.CurrentValue + damage}," +
                         $"CurrentHealth: {newValue}",
                context: contextInfo,
                color: DColor.YELLOW,
                colorMessage: true);
        }
    }
}