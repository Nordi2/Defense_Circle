using _Project.Cor.Component;
using _Project.Cor.Interfaces;
using _Project.Cor.Observers;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Cor.BulletLogic.Mono
{
    public class BulletFacade : MonoBehaviour
    {
        [SerializeField] private TakeDamageObserver _takeDamageObserver;
        [SerializeField] private int _damage;
        [SerializeField] private ParticleSystem _particle;
        
        private BulletMovement _movement;
        private GiveDamageComponent _giveDamageComponent;
        private readonly CompositeDisposable _disposable = new();

        [Inject]
        private void Construct(
            BulletMovement movement,
            GiveDamageComponent giveDamageComponent)
        {
            _giveDamageComponent = giveDamageComponent;
            _movement = movement;
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
            _giveDamageComponent.GiveDamage(takeDamagble, GiveDamageCallback);
        }

        private void GiveDamageCallback()
        {
            gameObject.SetActive(false);
        }
    }
}