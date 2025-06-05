using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.EnemyLogic;
using _Project.Scripts.Gameplay.Tower;
using DebugToolsPlus;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay
{
    [UsedImplicitly]
    public class CheatManager : MonoBehaviour
    {
        [FormerlySerializedAs("EnemyFastPrefab")] [Header("Spawn Settings")] public EnemyFacade enemyFacadeFastPrefab;
        [FormerlySerializedAs("EnemyDefaultPrefab")] public EnemyFacade enemyFacadeDefaultPrefab;
        [FormerlySerializedAs("EnemySlowPrefab")] public EnemyFacade enemyFacadeSlowPrefab;

        private TowerFacade _towerFacade;
        private IInstantiator _instantiator;

        private List<EnemyFacade> _enemiesInSpawned = new();

        [Inject]
        private void Construct(
            TowerFacade towerFacade,
            IInstantiator instantiator)
        {
            _towerFacade = towerFacade;
            _instantiator = instantiator;
        }

        public void SpawnEnemy(EnemyType type)
        {
            Vector3 randomPosition = GetRandomSpawnPosition();

            switch (type)
            {
                case EnemyType.Default:
                    CreateEnemyAndGetLog(randomPosition, "Create: Default-Enemy\n", enemyFacadeDefaultPrefab);
                    break;
                case EnemyType.Fast:
                    CreateEnemyAndGetLog(randomPosition, "Create: Fast-Enemy\n", enemyFacadeFastPrefab);
                    break;
                case EnemyType.Slow:
                    CreateEnemyAndGetLog(randomPosition, "Create: Slow-Enemy\n", enemyFacadeSlowPrefab);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void CreateEnemyAndGetLog(Vector3 randomPosition, string log, EnemyFacade prefab)
        {
            EnemyFacade enemyFacade = _instantiator.InstantiatePrefab(prefab, randomPosition, Quaternion.identity, null)
                .GetComponent<EnemyFacade>();

            enemyFacade.OnDeath += DeleteFromList;
            _enemiesInSpawned.Add(enemyFacade);
            
            D.Log(GetType().Name.ToUpper(), log + D.FormatText(enemyFacade.ShowStats.ToString(), DColor.RED),
                enemyFacade.gameObject, DColor.AQUAMARINE, true);
        }

        private void DeleteFromList(EnemyFacade concreteEnemyFacade)
        {
            concreteEnemyFacade.OnDeath -= DeleteFromList;
            _enemiesInSpawned.Remove(concreteEnemyFacade);
        }

        public void KillRandomSpawnedEnemies()
        {
            if (_enemiesInSpawned.Count == 0)
            {
                D.LogWarning(GetType().Name.ToUpper(), "List is empty.",DColor.AQUAMARINE,true);
                return;
            }

            int randomIndex = Random.Range(0, _enemiesInSpawned.Count);

            _enemiesInSpawned[randomIndex].TakeDamage(int.MaxValue);
        }

        public void KillAllSpawnedEnemies()
        {
            if (_enemiesInSpawned.Count == 0)
            {
                D.LogWarning(GetType().Name.ToUpper(),"List is empty.",DColor.AQUAMARINE,true);
                return;
            }

            List<EnemyFacade> enemiesToKill = new List<EnemyFacade>(_enemiesInSpawned);

            foreach (EnemyFacade enemy in enemiesToKill)
            {
                if (enemy != null)
                    enemy.TakeDamage(int.MaxValue);
            }
        }

        public void AddMoney(int amount)
        {
            Debug.Log($"Добавлено {amount} денег");
        }

        public void TakeDamage(int amount)
        {
            D.Log(GetType().Name.ToUpper(), $"Take Damage: {amount}", DColor.AQUAMARINE, true);
            _towerFacade.TakeDamage(amount);
        }

        public void HealPlayer(int amount)
        {
            Debug.Log($"Восстановлено {amount} здоровья");
        }

        private Vector3 GetRandomSpawnPosition()
        {
            Vector2 direction = Random.insideUnitCircle.normalized;

            float minDistance = 5f;
            float maxDistance = 12;

            float distance = Random.Range(minDistance, maxDistance);

            return direction * distance;
        }
    }
}