using System;
using _Project.Scripts.Gameplay.Stats;
using _Project.Scripts.Gameplay.TowerLogic;

namespace _Project.Scripts.Gameplay.Component
{
    public class HealthComponent
    {
        private Action _takeDamageCallback;
        private HealthStat _healthStat;

        public HealthComponent(HealthStat healthStat,ITakeDamagbleCallback takeDamageCallback)
        {
            _healthStat = healthStat;
            _takeDamageCallback = takeDamageCallback.TakeDamage;
        }
    }
}