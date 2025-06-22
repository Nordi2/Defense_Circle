using _Project.Meta.StatsLogic;
using _Project.Meta.StatsLogic.Upgrade;
using Zenject;

namespace _Project.Cor.Component
{
    public class PassiveHealthComponent : 
        ITickable
    {
        private readonly StatsStorage _statsStorage;
        private readonly HealthStats _healthStats;

        private float _accumulationHealth;
        
        public PassiveHealthComponent(
            HealthStats healthStats,
            StatsStorage statsStorage)
        {
            _healthStats = healthStats;
            _statsStorage = statsStorage;
        }

        private bool _isFullHealth => 
            _healthStats.CurrentHealth.CurrentValue >= _healthStats.MaxHealth.CurrentValue;

        void ITickable.Tick()
        {
            if(_isFullHealth)
                return;
            
            AccumulationPassiveHealth();
        }

        private void AccumulationPassiveHealth()
        {
            _accumulationHealth += _statsStorage.GetStats<PassiveHealthStats>().CurrentValue;

            if (!(_accumulationHealth >= 1)) 
                return;
            
            _healthStats.AddHealth((int)_accumulationHealth);
            _accumulationHealth = 0;
        }
    }
}