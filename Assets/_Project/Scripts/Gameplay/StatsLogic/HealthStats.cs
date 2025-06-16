using _Project.Scripts.Gameplay.Stats;
using JetBrains.Annotations;
using R3;

namespace _Project.Scripts.Gameplay.StatsLogic
{
    [UsedImplicitly]
    public class HealthStats :
        IUpgradableStats
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

        public int CurrentLevel { get; private set; }

        public void SetCurrentHealthValue(int newValue) =>
            _currentHealth.Value = newValue;

        public void UpgradeStats(int newValue)
        {
            CurrentLevel++;
            _maxHealth.Value = newValue;
        }

        public string ShowInfo() =>
            $"MaxHealth: {_maxHealth.Value}. ";
    }
}