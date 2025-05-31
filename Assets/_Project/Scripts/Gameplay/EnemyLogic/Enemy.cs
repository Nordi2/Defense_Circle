using _Project.Scripts.Gameplay.Observers;
using _Project.Scripts.Gameplay.TowerLogic;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.EnemyLogic
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private TowerObserver _towerObserver;

        private readonly CompositeDisposable _disposable = new();

        [Inject]
        private void Construct(EnemyStats stats)
        {
            Stats = stats;
        }

        public EnemyStats Stats {get; private set;}

        private void OnEnable()
        {
            _towerObserver
                .TriggerEnter
                .Subscribe(TriggerEnter)
                .AddTo(_disposable);
        }

        private void OnDisable() =>
            _disposable.Dispose();

        private void TriggerEnter(Tower tower)
        {
            tower.TakeDamage(Stats.CollisionDamage);
        }
    }
}