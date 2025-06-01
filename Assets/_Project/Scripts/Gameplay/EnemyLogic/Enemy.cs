using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Gameplay.Observers;
using _Project.Scripts.Gameplay.Stats.EnemyStats;
using _Project.Scripts.Gameplay.TowerLogic;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.EnemyLogic
{
    public class Enemy : MonoBehaviour,
        ITakeDamagble
    {
        [SerializeField] private TakeDamageObserver _takeDamageObserver;

        private TakeDamageComponent _takeDamageComponent;
        private GiveDamageComponent _giveDamageComponent;
        private EnemyView _view;
        private readonly CompositeDisposable _disposable = new();

        [Inject]
        private void Construct(
            TakeDamageComponent takeDamageComponent,
            GiveDamageComponent giveDamageComponent,
            ShowStats showStats,
            EnemyView view)
        {
            ShowStats = showStats;

            _view = view;
            _giveDamageComponent = giveDamageComponent;
            _takeDamageComponent = takeDamageComponent;
        }

        public ShowStats ShowStats { get; private set; }

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
            Debug.Log("Callback");
        }

        private void DieCallback()
        {
            gameObject.SetActive(false);
        }

        private void TakeDamageCallback(int damageValue)
        {
            DamageText damageText = Instantiate(_view.DamageTextPrefab, _view.transform.position, _view.transform.rotation);
            damageText.StartAnimation(damageValue);
        }
    }
}