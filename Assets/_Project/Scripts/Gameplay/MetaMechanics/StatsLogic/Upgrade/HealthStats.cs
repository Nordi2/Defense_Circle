using JetBrains.Annotations;
using R3;

namespace Meta.Stats.Upgrade
{
    [UsedImplicitly]
    public class HealthStats
    {
        private readonly ReactiveProperty<int> _maxHealth;
        private readonly ReactiveProperty<int> _currentHealth;

        public ReadOnlyReactiveProperty<int> MaxHealth => _maxHealth;
        public ReadOnlyReactiveProperty<int> CurrentHealth => _currentHealth;

        public HealthStats(int maxHealth)
        {
            _maxHealth = new ReactiveProperty<int>(maxHealth);
            _currentHealth = new ReactiveProperty<int>(maxHealth);
        }
        
        public void SetCurrentHealthValue(int newValue) =>
            _currentHealth.Value = newValue;

        public string ShowInfo() =>
            $"MaxHealth: {_maxHealth.Value}. ";
    }
}