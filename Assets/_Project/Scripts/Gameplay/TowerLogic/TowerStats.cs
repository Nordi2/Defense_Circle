using _Project.Scripts.Data;
using R3;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    public class TowerStats 
    {
        private ReactiveProperty<int> _maxHealth;
        private ReactiveProperty<float> _shootRate;
        private ReactiveProperty<int> _countTarget;

        private TowerConfig _config;

        public TowerStats(TowerConfig config)
        {
            _config = config;

            _maxHealth = new ReactiveProperty<int>(config.MaxHealth);
            _countTarget = new ReactiveProperty<int>(config.AmountTarget);
            _shootRate = new ReactiveProperty<float>(config.ShootRate);
        }
    }
}