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
        private AnimationTower _animationTower;
        
        [Inject]
        private void Construct(
            HealthComponent healthComponent,
            AnimationTower animationTower)
        {
            _healthComponent = healthComponent;
            _animationTower = animationTower;
        }

        public void TakeDamage(int damage)
        {
            _animationTower.PlayAnimationTakeDamage();
            _healthComponent.TakeDamage(damage);
        }

        public Vector2 GetPosition() => 
            transform.position;
    }
}