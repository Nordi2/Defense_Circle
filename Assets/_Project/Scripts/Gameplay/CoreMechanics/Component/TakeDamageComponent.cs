using System;
using _Project.Meta.StatsLogic.Upgrade;
using DebugToolsPlus;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Cor.Component
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
            

            D.Log($"{GetType().Name}({type?.Name})",
                D.FormatText(
                    $"\nTakeDamage: {damage}," +
                    $"MaxHealth: {_healthStat.MaxHealth.CurrentValue}," +
                    $"OldHealth: {Math.Clamp(_healthStat.CurrentHealth.CurrentValue + damage, 0, _healthStat.MaxHealth.CurrentValue)}," +
                    $"CurrentHealth: {newValue}", DColor.RED),
                contextInfo,
                DColor.YELLOW);
        }
    }
}