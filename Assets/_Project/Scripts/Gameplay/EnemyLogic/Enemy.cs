using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Gameplay.Observers;
using _Project.Scripts.Gameplay.Stats.EnemyStats;
using _Project.Scripts.Gameplay.TowerLogic;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.EnemyLogic
{
    public class Enemy : MonoBehaviour ,
        ITakeDamagble
    {
        [SerializeField] private TakeDamageObserver _takeDamageObserver;

        private readonly CompositeDisposable _disposable = new();
        private CollisionDamageStat _collisionDamage;
        private TakeDamageComponent _takeDamageComponent;

        [Inject]
        private void Construct(
            CollisionDamageStat collisionDamageStat,
            TakeDamageComponent takeDamageComponent)
        {
            _collisionDamage = collisionDamageStat;
            _takeDamageComponent = takeDamageComponent;
        }

        private void OnEnable()
        {
            _takeDamageObserver
                .TriggerEnter
                .Subscribe(CollisionTakeDamageObject)
                .AddTo(_disposable);
        }

        private void OnDisable() =>
            _disposable.Dispose();

        private void CollisionTakeDamageObject(ITakeDamagble takeDamageObject)
        {
            takeDamageObject.TakeDamage(_collisionDamage.Damage);
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

        private void DieCallback()
        {
            gameObject.SetActive(false);
        }

        private void TakeDamageCallback()
        {
        }
    }
}