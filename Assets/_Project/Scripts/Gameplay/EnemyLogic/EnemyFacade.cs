using System;
using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Gameplay.Money;
using _Project.Scripts.Gameplay.Observers;
using _Project.Scripts.Gameplay.Tower;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.EnemyLogic
{
    public class EnemyFacade : MonoBehaviour,
        ITakeDamagble
    {
        public event Action<EnemyFacade> OnDeath;

        [SerializeField] private TakeDamageObserver _takeDamageObserver;

        private TakeDamageComponent _takeDamageComponent;
        private GiveDamageComponent _giveDamageComponent;
        private EnemyView _view;
        private AnimationEnemy _animation;
        private IDisposable _disposable;

        [Inject]
        private void Construct(
            TakeDamageComponent takeDamageComponent,
            GiveDamageComponent giveDamageComponent,
            ShowStats showStats,
            EnemyView view,
            AnimationEnemy animation)
        {
            _takeDamageComponent = takeDamageComponent;
            _giveDamageComponent = giveDamageComponent;
            _view = view;
            _animation = animation;
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
        }

        public void TakeDamage(int damage)
        {
            _takeDamageComponent.TakeDamage(
                damage: damage,
                out bool isDie,
                takeDamageCallback: TakeDamageCallback,
                type: GetType(),
                contextInfo: gameObject);

            if (isDie)
                DieCallback();
        }

        private void GiveDamageCallback()
        {
            Instantiate(_view.DieEffect, transform.position, Quaternion.identity);
            OnDeath?.Invoke(this);
        }

        private void DieCallback()
        {
            Instantiate(_view.DieEffect, transform.position, Quaternion.identity);
            OnDeath?.Invoke(this);
        }

        private void TakeDamageCallback(int damageValue)
        {
            _animation.PlayTakeDamageAnimation();

            DamageText damageText =
                Instantiate(_view.DamageTextPrefab, _view.transform.position, _view.transform.rotation);
            damageText.StartAnimation(damageValue);
        }
    }
}