using System;
using _Project.Meta.Stats.Upgrade;
using DebugToolsPlus;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Cor.Component
{
    [UsedImplicitly]
    public class TakeDamageComponent
    {
        private readonly HealthStats _healthStats;

        public TakeDamageComponent(HealthStats healthStats)
        {
            _healthStats = healthStats;
        }

        public void TakeDamage(
            int damage,
            out bool isDie,
            Action<int> takeDamageCallback = null,
            Type type = null,
            GameObject contextInfo = null)
        {
            int newValue = Math.Clamp(
                _healthStats.CurrentHealth.CurrentValue - damage,
                0,
                _healthStats.MaxHealth.CurrentValue);

            _healthStats.SetCurrentHealthValue(newValue);

            isDie = newValue <= 0;

            if (!isDie)
                takeDamageCallback?.Invoke(damage);
            

            D.Log($"{GetType().Name}({type?.Name})",
                D.FormatText(
                    $"\nTakeDamage: {damage}," +
                    $"MaxHealth: {_healthStats.MaxHealth.CurrentValue}," +
                    $"OldHealth: {Math.Clamp(_healthStats.CurrentHealth.CurrentValue + damage, 0, _healthStats.MaxHealth.CurrentValue)}," +
                    $"CurrentHealth: {newValue}", DColor.RED),
                contextInfo,
                DColor.YELLOW);
        }
    }
}