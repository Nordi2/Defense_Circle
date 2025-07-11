using _Project.Cor.Component;
using _Project.Cor.Interfaces;
using Infrastructure.Services;
using UnityEngine;

namespace _Project.Cor.Tower.Mono
{
    public class TowerFacade : MonoBehaviour,
        IGameStartListener,
        ITakeDamagble
    {
        private TowerCallbacks _towerCallbacks;
        private TakeDamageComponent _takeDamageComponent;
        private RecoverComponent _recoverComponent;
        
        public void Init(
            TakeDamageComponent takeDamageComponent,
            TowerCallbacks towerCallbacks,
            RecoverComponent recoverComponent)
        {
            _towerCallbacks = towerCallbacks;
            _takeDamageComponent = takeDamageComponent;
            _recoverComponent = recoverComponent;
        }

        public void OnGameStart()
        {
            _towerCallbacks.GameStart(gameObject);
        }

        public void RecoverTower(int amount) => 
            _recoverComponent.Recover(amount);

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