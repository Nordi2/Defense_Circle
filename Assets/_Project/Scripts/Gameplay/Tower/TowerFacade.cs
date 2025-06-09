using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Gameplay.Tower.Callbacks;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Tower
{
    public class TowerFacade : MonoBehaviour,
        IGameStartListener,
        ITakeDamagble
    {
        private TowerCallbacks _towerCallbacks;
        private TakeDamageComponent _takeDamageComponent;

        [Inject]
        private void Construct(
            TakeDamageComponent takeDamageComponent,
            TowerCallbacks towerCallbacks)
        {
            _towerCallbacks = towerCallbacks;
            _takeDamageComponent = takeDamageComponent;
        }

        public void OnGameStart() =>
            _towerCallbacks.GameStart(gameObject);
        
        public void TakeDamage(int damage)
        {
            _takeDamageComponent.TakeDamage(
                damage,
                out bool isDie,
                _towerCallbacks.TakeDamageCallback,
                GetType(),
                gameObject);

            if (isDie)
                _towerCallbacks.DeathCallback();
        }
    }
}