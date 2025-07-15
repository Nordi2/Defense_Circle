using System;
using _Project.Cor.Tower.Mono;
using _Project.Meta.StatsLogic;
using _Project.Meta.StatsLogic.Upgrade;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Cor.Tower
{
    [UsedImplicitly]
    public class TowerShoot :
        ITickable
    {
        public Action<Vector3, Transform> OnShoot;

        private readonly StatStorage _statStorage;
        private readonly EnemiesInCircle _enemiesInCircle;
        private readonly Transform _shootPoint;
        
        private readonly float _shootRate = 0.5f;
        private float _nextShootTime;

        public TowerShoot(
            EnemiesInCircle enemiesInCircle,
            TowerView view,
            StatStorage statStorage)
        {
            _enemiesInCircle = enemiesInCircle;
            _statStorage = statStorage;
            _shootPoint = view.ShootPoint;
        }

        void ITickable.Tick()
        {
            if (_enemiesInCircle.Enemies.Count <= 0)
                return;

            if (Time.time >= _nextShootTime)
            {
                Shoot();
                _nextShootTime = Time.time + 1f / _shootRate;
            }
        }

        private void Shoot()
        {
            for (int i = 0; i < _enemiesInCircle.Enemies.Count; i++)
            {
                if (i >= _statStorage.GetStatsValue<AmountTargetsStat>())
                    break;

                Vector3 positionEnemy = _enemiesInCircle.Enemies[i].transform.position;
                Vector3 direction = positionEnemy - _shootPoint.position;
                direction.z = 0;
                direction.Normalize();

                OnShoot?.Invoke(direction, _shootPoint);
            }
        }
    }
}