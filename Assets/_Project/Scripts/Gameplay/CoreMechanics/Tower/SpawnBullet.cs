using System;
using Cor.BulletLogic.Mono;
using Cor.Tower.Mono;
using Cor.BulletLogic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Cor.Tower
{
    [UsedImplicitly]
    public class SpawnBullet : 
        IInitializable,
        IDisposable
    {
        private readonly TowerShoot _towerShoot;
        private readonly BulletFacade _bulletPrefab;
        private readonly IInstantiator _instantiator;

        public SpawnBullet(
            TowerShoot towerShoot,
            TowerView view,
            IInstantiator instantiator)
        {
            _towerShoot = towerShoot;
            _instantiator = instantiator;
            _bulletPrefab = view.BulletPrefab;
        }

        void IInitializable.Initialize() => 
            _towerShoot.OnShoot += Spawn;

        void IDisposable.Dispose() => 
            _towerShoot.OnShoot -= Spawn;

        private void Spawn(Vector3 direction, Transform shootPoint)
        {
            BulletFacade bullet = _instantiator
                .InstantiatePrefab(_bulletPrefab, shootPoint.position, Quaternion.identity, null)
                .GetComponent<BulletFacade>();

            bullet.Initialize(shootPoint, direction);
        }
    }
}