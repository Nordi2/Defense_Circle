using System.Collections.Generic;
using _Project.Cor.Enemy.Mono;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services
{
    [UsedImplicitly]
    public class EnemyPool : MonoMemoryPool<Vector3, EnemyFacade>
    {
        public List<EnemyFacade> SpawnedEnemies = new(); 
        
        protected override void OnCreated(EnemyFacade item)
        {
            base.OnCreated(item);
        }

        protected override void OnSpawned(EnemyFacade enemy)
        {
            SpawnedEnemies.Add(enemy);
            base.OnSpawned(enemy);
        }

        protected override void Reinitialize(Vector3 spawnPosition, EnemyFacade enemy)
        {
            base.Reinitialize(spawnPosition, enemy);

            enemy.transform.position = spawnPosition;
            enemy.OnDeath += OnDespawned;
        }

        protected override void OnDespawned(EnemyFacade item)
        {
            base.OnDespawned(item);
            SpawnedEnemies.Remove(item);
            item.OnDeath -= OnDespawned;
        }
    }
}