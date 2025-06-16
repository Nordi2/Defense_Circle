using System;
using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Gameplay.Tower;
using Cor.Component;
using Cor.Interfaces;
using Cor.Observers;
using R3;
using UnityEngine;
using Zenject;

namespace Cor.Enemy.Mono
{
    public class EnemyFacade : MonoBehaviour,
        ITakeDamagble
    {
        public event Action<EnemyFacade> OnDeath;

        [SerializeField] private TakeDamageObserver _takeDamageObserver;

        private TakeDamageComponent _takeDamageComponent;
        private GiveDamageComponent _giveDamageComponent;
        private EnemyCallbacks _enemyCallbacks;
        private IDisposable _disposable;
        
        [Inject]
        private void Construct(
            TakeDamageComponent takeDamageComponent,
            GiveDamageComponent giveDamageComponent,
            ShowStats showStats,
            EnemyCallbacks enemyCallbacks)
        {
            _enemyCallbacks = enemyCallbacks;
            _takeDamageComponent = takeDamageComponent;
            _giveDamageComponent = giveDamageComponent;
            ShowStats = showStats;
        }

        public ShowStats ShowStats { get; private set; }

        private void OnEnable()
        {
            _disposable = _takeDamageObserver
                .TriggerEnter
                .Subscribe(CollisionTakeDamageObject);
        }

        private void OnDisable() =>
            _disposable.Dispose();

        private void CollisionTakeDamageObject(ITakeDamagble takeDamageObject)
        {
            _giveDamageComponent.GiveDamage(takeDamageObject, GiveDamageCallback);
            gameObject.SetActive(false);
        }

        public void TakeDamage(int damage)
        {
            _takeDamageComponent.TakeDamage(
                damage: damage,
                out bool isDie,
                takeDamageCallback: _enemyCallbacks.TakeDamageCallback,
                type: GetType(),
                contextInfo: gameObject);

            if (isDie)
            {
                _enemyCallbacks.DieCallback();
                gameObject.SetActive(false);
                OnDeath?.Invoke(this);
            }
        }

        private void GiveDamageCallback()
        {
            OnDeath?.Invoke(this);
        }

        private void DieCallback()
        {
            OnDeath?.Invoke(this);
        }

        private void TakeDamageCallback(int damageValue)
        {
           
        }
    }
}