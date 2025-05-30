using System;
using _Project.Scripts.Gameplay.BulletLogic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    public class SpawnBullet : 
        IInitializable ,
        IDisposable
    {
        private readonly TowerShoot _towerShoot;
        private readonly Bullet _bulletPrefab;

        public SpawnBullet(TowerShoot towerShoot, Bullet bulletPrefab)
        {
            _towerShoot = towerShoot;
            _bulletPrefab = bulletPrefab;
        }

        void IInitializable.Initialize()
        {
            _towerShoot.OnShoot += Spawn;
        }

        public void Dispose()
        {
            _towerShoot.OnShoot -= Spawn;
        }

        private void Spawn(Vector3 direction,Transform shootPoint)
        {
           Bullet bullet = Object.Instantiate(_bulletPrefab, shootPoint.position, Quaternion.identity);
           bullet.targetPosition = direction;
        }
    }
}