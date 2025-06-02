using System;
using _Project.Scripts.Gameplay.BulletLogic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    [UsedImplicitly]
    public class SpawnBullet :
        IInitializable,
        IDisposable
    {
        private readonly TowerShoot _towerShoot;
        private readonly Bullet _bulletPrefab;
        private readonly IInstantiator _instantiator;

        public SpawnBullet(TowerShoot towerShoot, TowerView view, IInstantiator instantiator)
        {
            _towerShoot = towerShoot;
            _instantiator = instantiator;
            _bulletPrefab = view.BulletPrefab;
        }

        void IInitializable.Initialize()
        {
            _towerShoot.OnShoot += Spawn;
        }

        public void Dispose()
        {
            _towerShoot.OnShoot -= Spawn;
        }

        private void Spawn(Vector3 direction, Transform shootPoint)
        {
            Bullet bullet = _instantiator
                .InstantiatePrefab(_bulletPrefab, shootPoint.position, Quaternion.identity, null)
                .GetComponent<Bullet>();

            bullet.Initialize(shootPoint, direction);
        }
    }
}