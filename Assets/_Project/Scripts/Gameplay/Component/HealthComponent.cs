using DebugToolsPlus;
using JetBrains.Annotations;
using R3;

namespace _Project.Scripts.Gameplay.Component
{
    [UsedImplicitly]
    public class HealthComponent
    {
        private int _currentHealth;
        private int _maxHealth;

        public readonly Subject<(int, int)> OnHealthChanged = new();
        public int CurrentHealth => _currentHealth;
        
        public HealthComponent(int maxHeath)
        {
            _currentHealth = maxHeath;
            _maxHealth = maxHeath;
        }

        public void TakeDamage(int damage)
        {
            int oldValue = _currentHealth;

            _currentHealth -= damage;
            OnHealthChanged.OnNext((oldValue, _currentHealth));

            D.Log(GetType().Name,
                message: $"TakeDamage : {damage}, MaxHealth: {_maxHealth} , OldHealth: {oldValue}, CurrentHealth: {_currentHealth}",
                color: DColor.YELLOW,
                colorMessage: true);
        }
    }
}