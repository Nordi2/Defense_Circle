using _Project.Meta.StatsLogic;
using _Project.Meta.StatsLogic.Upgrade;
using Zenject;

namespace _Project.Cor.Component
{
    public class PassiveHealthComponent : 
        ITickable
    {
        private readonly StatStorage _statStorage;
        private readonly HealthStat _healthStat;

        private float _accumulationHealth;
        
        public PassiveHealthComponent(
            HealthStat healthStat,
            StatStorage statStorage)
        {
            _healthStat = healthStat;
            _statStorage = statStorage;
        }

        private bool _isFullHealth => 
            _healthStat.CurrentHealth.CurrentValue >= _healthStat.MaxHealth.CurrentValue;

        void ITickable.Tick()
        {
            if(_isFullHealth)
                return;
            
            AccumulationPassiveHealth();
        }

        private void AccumulationPassiveHealth()
        {
            _accumulationHealth += _statStorage.GetStatsValue<PassiveHealthStat>();

            if (!(_accumulationHealth >= 1)) 
                return;
            
            _healthStat.AddHealth((int)_accumulationHealth);
            _accumulationHealth = 0;
        }
    }
}