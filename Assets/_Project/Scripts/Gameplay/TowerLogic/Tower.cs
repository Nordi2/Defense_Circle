using _Project.Scripts.Gameplay.Component;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    public class Tower : MonoBehaviour ,
        IGetTargetPosition
    {
        [SerializeField] private int _maxHealth = 100;
        private HealthComponent _healthComponent;
        
        [Inject]
        private void Construct(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
        }

        public void TakeDamage(int damage)
        {
            _healthComponent.TakeDamage(damage);
        }

        public Vector2 GetPosition() => 
            transform.position;
    }
}