using _Project.Cor.Enemy.Mono;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services
{
    [UsedImplicitly]
    public class EnemyPool : MonoMemoryPool<Vector3, EnemyFacade>
    {
        protected override void Reinitialize(Vector3 spawnPosition, EnemyFacade enemy)
        {
            base.Reinitialize(spawnPosition, enemy);

            enemy.transform.position = spawnPosition;
            enemy.OnDeath += OnDespawned;
        }

        protected override void OnDespawned(EnemyFacade item)
        {
            base.OnDespawned(item);
            item.OnDeath -= OnDespawned;
        }
    }
}