using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    public class Tower : MonoBehaviour,
        IGameStartListener,
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

        public void OnGameStart() => 
            _animationTower.PlayInitialSpawn(() => gameObject.SetActive(true));

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
            _animationTower.PlayDeath();

        private void TakeDamageCallback(int damage) => 
            _animationTower.PlayTakeDamage();
    }
}