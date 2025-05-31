using System;
using _Project.Scripts.Gameplay.EnemyLogic;
using _Project.Scripts.Gameplay.Observers;
using R3;
using UnityEngine;

namespace _Project.Scripts.Gameplay.BulletLogic
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private EnemyObserver _enemyObserver;
        [SerializeField] private int _damage;
        public Vector3 targetPosition;
        public float _moveSpeed;
        public Rigidbody2D _Rigidbody2D;

        private readonly CompositeDisposable _disposable = new();

        private void OnEnable()
        {
            _enemyObserver.TriggerEnter.Subscribe(DeactivateBullet).AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }

        private void DeactivateBullet(Enemy enemy)
        {
            Debug.Log("Damage");
        }

        private void Update()
        {
            transform.position += targetPosition * (_moveSpeed * Time.deltaTime);
        }
    }
}