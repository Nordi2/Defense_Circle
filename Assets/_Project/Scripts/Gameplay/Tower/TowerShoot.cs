using System;
using _Project.Scripts.Gameplay.StatsLogic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Tower
{
    [UsedImplicitly]
    public class TowerShoot : 
        ITickable
    {
        public Action<Vector3, Transform> OnShoot;

        private readonly EnemysVault _enemysVault;
        private readonly Transform _shootPoint;
        private readonly StatsStorage _statsStorage;
        
        private readonly int _initialCountTarget = 2;
        
        private readonly float _shootRate = 0.5f;
        private float _nextShootTime;

        public TowerShoot(
            EnemysVault enemysVault,
            TowerView view, 
            StatsStorage statsStorage)
        {
            _enemysVault = enemysVault;
            _statsStorage = statsStorage;
            _shootPoint = view.ShootPoint;
        }
        
        void ITickable.Tick()
        {
            if (_enemysVault.Enemies.Count <= 0)
                return;
            
            if (Time.time >= _nextShootTime)
            {
                Shoot();
                _nextShootTime = Time.time + 1f / _shootRate;
            }   
        }

        private void Shoot()
        {
            for (int i = 0; i < _enemysVault.Enemies.Count; i++)
            {
                if (i >= _statsStorage.CountTargetsValue)
                    break;

                Vector3 positionEnemy = _enemysVault.Enemies[i].transform.position;
                Vector3 direction = positionEnemy - _shootPoint.position;
                direction.z = 0;
                direction.Normalize();

                OnShoot?.Invoke(direction, _shootPoint);
            }
        }
    }
}