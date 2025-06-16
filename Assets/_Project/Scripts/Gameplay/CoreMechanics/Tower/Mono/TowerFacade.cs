using Cor.Component;
using Cor.Interfaces;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Cor.Tower.Mono
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