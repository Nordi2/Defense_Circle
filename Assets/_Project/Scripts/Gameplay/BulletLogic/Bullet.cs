using _Project.Scripts.Gameplay.EnemyLogic;
using _Project.Scripts.Gameplay.Observers;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.BulletLogic
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private EnemyObserver _enemyObserver;
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
            _enemyObserver
                .TriggerEnter
                .Subscribe(DeactivateBullet)
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

        private void DeactivateBullet(Enemy enemy)
        {
            gameObject.SetActive(false);
            Debug.Log("Damage");
        }
    }
}