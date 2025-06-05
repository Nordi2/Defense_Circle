using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.EnemyLogic;
using _Project.Scripts.Gameplay.TowerLogic;
using DebugToolsPlus;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay
{
    [UsedImplicitly]
    public class CheatManager : MonoBehaviour
    {
        [Header("Spawn Settings")] public Enemy EnemyFastPrefab;
        public Enemy EnemyDefaultPrefab;
        public Enemy EnemySlowPrefab;

        private TowerFacade _towerFacade;
        private IInstantiator _instantiator;

        private List<Enemy> _enemiesInSpawned = new();

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
                    CreateEnemyAndGetLog(randomPosition, "Create: Default-Enemy\n", EnemyDefaultPrefab);
                    break;
                case EnemyType.Fast:
                    CreateEnemyAndGetLog(randomPosition, "Create: Fast-Enemy\n", EnemyFastPrefab);
                    break;
                case EnemyType.Slow:
                    CreateEnemyAndGetLog(randomPosition, "Create: Slow-Enemy\n", EnemySlowPrefab);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void CreateEnemyAndGetLog(Vector3 randomPosition, string log, Enemy prefab)
        {
            Enemy enemy = _instantiator.InstantiatePrefab(prefab, randomPosition, Quaternion.identity, null)
                .GetComponent<Enemy>();

            enemy.OnDeath += DeleteFromList;
            _enemiesInSpawned.Add(enemy);
            
            D.Log(GetType().Name.ToUpper(), log + D.FormatText(enemy.ShowStats.ToString(), DColor.RED),
                enemy.gameObject, DColor.AQUAMARINE, true);
        }

        private void DeleteFromList(Enemy concreteEnemy)
        {
            concreteEnemy.OnDeath -= DeleteFromList;
            _enemiesInSpawned.Remove(concreteEnemy);
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

            List<Enemy> enemiesToKill = new List<Enemy>(_enemiesInSpawned);

            foreach (Enemy enemy in enemiesToKill)
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