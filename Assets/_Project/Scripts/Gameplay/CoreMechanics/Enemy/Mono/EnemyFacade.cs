using System;
using _Project.Cor.Component;
using _Project.Cor.Interfaces;
using _Project.Cor.Observers;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Cor.Enemy.Mono
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


        public void Init(
            TakeDamageComponent takeDamageComponent,
            GiveDamageComponent giveDamageComponent,
            EnemyCallbacks enemyCallbacks)
        {
            _enemyCallbacks = enemyCallbacks;
            _takeDamageComponent = takeDamageComponent;
            _giveDamageComponent = giveDamageComponent;
        }
        
        public ShowStatsService ShowStatsService { get; private set; }

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
            _enemyCallbacks.GiveDamageCallback();
        }

        private void DieCallback()
        {
            OnDeath?.Invoke(this);
            _enemyCallbacks.DieCallback();
        }
    }
}