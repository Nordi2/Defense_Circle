using DebugToolsPlus;
using JetBrains.Annotations;
using R3;

namespace _Project.Scripts.Gameplay.Component
{
    [UsedImplicitly]
    public class HealthComponent
    {
        private readonly ReactiveProperty<int> _maxHealth;
        private readonly ReactiveProperty<int> _currentHealth;
        
        public ReadOnlyReactiveProperty<int> MaxHealth => _maxHealth;
        public ReadOnlyReactiveProperty<int> CurrentHealth => _currentHealth;

        public HealthComponent(int maxHeath)
        {
            _maxHealth = new ReactiveProperty<int>(maxHeath);
            _currentHealth = new ReactiveProperty<int>(maxHeath);
        }

        public void TakeDamage(int damage)
        {
            D.Log(GetType().Name,
                message: $"TakeDamage : {damage}, HadHealth: {_maxHealth.Value} , WillHealth: {_currentHealth.Value - damage}",
                color: DColor.YELLOW,
                colorMessage: true);
            
            _currentHealth.Value -= damage;
        }
    }
}