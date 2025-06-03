using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using DG.Tweening;
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
        private EnemysVault _enemysVault;
        
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

        public void OnGameStart()
        {
            _animationTower.InitialSpawnAnimation.Play();
        }
        
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

        private void DieCallback()
        {
            //_animationTower.PlayDeath(() => gameObject.SetActive(false));
        }

        private void TakeDamageCallback(int damage)
        {
            _animationTower.PlayTakeDamage();
        }
    }
}