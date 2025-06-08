using System.Collections.Generic;
using _Project.Scripts.Gameplay.EnemyLogic;
using _Project.Scripts.Gameplay.Tower;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.Services.Pools
{
    [UsedImplicitly]
    public class EnemyPool : MonoMemoryPool<Vector3, EnemyFacade>
    {
        protected override void OnCreated(EnemyFacade item)
        {
            base.OnCreated(item);
        }

        protected override void OnSpawned(EnemyFacade item)
        {
            base.OnSpawned(item);
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
            item.OnDeath -= OnDespawned;
        }
    }
}