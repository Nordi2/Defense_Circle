using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Gameplay.EnemyLogic;
using _Project.Scripts.Gameplay.Observers;
using _Project.Scripts.Gameplay.TowerLogic;
using DebugToolsPlus;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.BulletLogic
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private TakeDamageObserver _takeDamageObserver;
        [SerializeField] private int _damage;
        [SerializeField] private ParticleSystem _particle;
        private BulletMovement _movement;
        private GiveDamageComponent _giveDamageComponent;
        private ShowStats _showStats;
        private readonly CompositeDisposable _disposable = new();

        [Inject]
        private void Construct(
            BulletMovement movement,
            GiveDamageComponent giveDamageComponent,
            ShowStats showStats)
        {
            _giveDamageComponent = giveDamageComponent;
            _movement = movement;
            _showStats = showStats;
        }

        private void OnEnable()
        {
            _takeDamageObserver
                .TriggerEnter
                .Subscribe(CollisionTakeDamageObject)
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }

        public void Initialize(
            Transform spawnPoint,
            Vector3 targetPosition)
        {
            D.Log(GetType().Name, D.FormatText($"\n{_showStats}", DColor.RED), gameObject, DColor.YELLOW);
            transform.position = spawnPoint.position;
            _movement.Initialize(targetPosition);
        }

        private void CollisionTakeDamageObject(ITakeDamagble takeDamagble)
        {
            _giveDamageComponent.GiveDamage(takeDamagble, GiveDamageCallback);
        }

        private void GiveDamageCallback()
        {
            gameObject.SetActive(false);
        }
    }
}