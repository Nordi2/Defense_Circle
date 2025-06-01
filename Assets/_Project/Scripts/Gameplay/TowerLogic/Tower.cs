using _Project.Scripts.Gameplay.Component;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    public class Tower : MonoBehaviour,
        ITakeDamagble,
        IGetTargetPosition
    {
        private AnimationTower _animationTower;
        private TakeDamageComponent _takeDamageComponent;

        [Inject]
        private void Construct(
            AnimationTower animationTower,
            TakeDamageComponent takeDamageComponent)
        {
            _takeDamageComponent = takeDamageComponent;
            _animationTower = animationTower;
        }

        public Vector2 GetPosition() =>
            transform.position;

        public void TakeDamage(int damage)
        {
            _takeDamageComponent.TakeDamage(
                damage,
                out bool isDie,
                TakeDamageCallback,
                GetType(),
                gameObject);

            if (isDie)
                DieCallback();
        }

        private void DieCallback() => 
            gameObject.SetActive(false);

        private void TakeDamageCallback(int damage)
        {
            _animationTower.PlayAnimationTakeDamage();
        }
    }
}