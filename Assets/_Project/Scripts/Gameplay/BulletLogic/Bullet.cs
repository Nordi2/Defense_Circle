using _Project.Scripts.Gameplay.EnemyLogic;
using _Project.Scripts.Gameplay.Observers;
using _Project.Scripts.Gameplay.TowerLogic;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.BulletLogic
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private TakeDamageObserver _takeDamageObserver;
        [SerializeField] private int _damage;
        private BulletMovement _movement;
        
        private readonly CompositeDisposable _disposable = new();
        
        [Inject]
        private void Construct(BulletMovement movement)
        {
            _movement   = movement;
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
            transform.position = spawnPoint.position;
            _movement.Initialize(targetPosition);
        }

        private void CollisionTakeDamageObject(ITakeDamagble takeDamagble)
        {
            gameObject.SetActive(false);
            takeDamagble.TakeDamage(_damage);
        }
    }
}