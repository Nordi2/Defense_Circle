using _Project.Scripts.Gameplay.Observers;
using _Project.Scripts.Gameplay.TowerLogic;
using R3;
using UnityEngine;

namespace _Project.Scripts.Gameplay.EnemyLogic
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 2.5f;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _damage = 10f;
        [SerializeField] private TowerObserver _towerObserver;

        private readonly CompositeDisposable _disposable = new();

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
            tower.TakeDamage((int)_damage);
        }
    }
}