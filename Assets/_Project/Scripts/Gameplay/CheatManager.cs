#if UNITY_EDITOR
using System.Collections.Generic;
using _Project.Scripts.Gameplay.EnemyLogic;
using _Project.Scripts.Gameplay.Money;
using _Project.Scripts.Gameplay.Tower;
using _Project.Scripts.Infrastructure.Services.Pools;
using DebugToolsPlus;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay
{
    [UsedImplicitly]
    public static class CheatManager
    {
        private const string CheathManager = "CheathManager";

        public static EnemyPool EnemyPool;
        public static TowerFacade TowerFacade;
        public static Wallet Wallet;

        private static readonly List<EnemyFacade> _enemiesInSpawned = new();

        public static void SpawnEnemy(EnemyType type)
        {
            Vector3 randomPosition = GetRandomSpawnPosition();

            SpawnEnemyAndGetLog(randomPosition, "Create Enemy\n");
            /*switch (type)
            {
                case EnemyType.Default:
                    if (EnemyFacadeDefaultPrefab == null)
                    {
                        D.LogWarning(CheathManager.ToUpper(), "Reference to prefab is null. Start playing",
                            DColor.AQUAMARINE, true);
                        return;
                    }

                    CreateEnemyAndGetLog(randomPosition, "Create: Default-Enemy\n", EnemyFacadeDefaultPrefab);
                    break;
                case EnemyType.Fast:
                    if (EnemyFacadeFastPrefab == null)
                    {
                        D.LogWarning(CheathManager.ToUpper(), "Reference to prefab is null. Start playing",
                            DColor.AQUAMARINE, true);
                        return;
                    }

                    CreateEnemyAndGetLog(randomPosition, "Create: Fast-Enemy\n", EnemyFacadeFastPrefab);
                    break;
                case EnemyType.Slow:
                    if (EnemyFacadeSlowPrefab == null)
                    {
                        D.LogWarning(CheathManager.ToUpper(), "Reference to prefab is null. Start playing",
                            DColor.AQUAMARINE, true);
                        return;
                    }

                    CreateEnemyAndGetLog(randomPosition, "Create: Slow-Enemy\n", EnemyFacadeSlowPrefab);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }*/
        }

        private static void SpawnEnemyAndGetLog(Vector3 randomPosition, string log)
        {
            EnemyFacade enemyFacade = EnemyPool.Spawn(randomPosition);
            enemyFacade.OnDeath += DeleteFromList;
            _enemiesInSpawned.Add(enemyFacade);

            D.Log(CheathManager.ToUpper(), log + D.FormatText(enemyFacade.ShowStats.ToString(), DColor.RED),
                enemyFacade.gameObject, DColor.AQUAMARINE, true);
        }

        private static void DeleteFromList(EnemyFacade concreteEnemyFacade)
        {
            concreteEnemyFacade.OnDeath -= DeleteFromList;
            _enemiesInSpawned.Remove(concreteEnemyFacade);
        }

        public static void KillRandomSpawnedEnemies()
        {
            if (_enemiesInSpawned.Count == 0)
            {
                D.LogWarning(CheathManager.ToUpper(), "List is empty.", DColor.AQUAMARINE, true);
                return;
            }

            int randomIndex = Random.Range(0, _enemiesInSpawned.Count);

            _enemiesInSpawned[randomIndex].TakeDamage(int.MaxValue);
        }

        public static void KillAllSpawnedEnemies()
        {
            if (_enemiesInSpawned.Count == 0)
            {
                D.LogWarning(CheathManager.ToUpper(), "List is empty.", DColor.AQUAMARINE, true);
                return;
            }

            List<EnemyFacade> enemiesToKill = new List<EnemyFacade>(_enemiesInSpawned);

            foreach (EnemyFacade enemy in enemiesToKill)
            {
                if (enemy != null)
                    enemy.TakeDamage(int.MaxValue);
            }
        }

        public static void AddMoney(int amount)
        {
            D.Log(CheathManager.ToUpper(), $"AddMoney: {amount}", DColor.AQUAMARINE, true);
            Wallet.AddMoney(amount);
        }

        public static void SpendMoney(int amount)
        {
            D.Log(CheathManager.ToUpper(), $"SpendMoney: {amount}", DColor.AQUAMARINE, true);
            Wallet.SpendMoney(amount);
        }

        public static void TakeDamage(int amount)
        {
            if (TowerFacade == null)
            {
                D.LogWarning(CheathManager.ToUpper(), "Reference to Tower is null. Start playing", DColor.AQUAMARINE,
                    true);
                return;
            }

            D.Log(CheathManager.ToUpper(), $"Take Damage: {amount}", DColor.AQUAMARINE, true);
            TowerFacade.TakeDamage(amount);
        }

        public static void HealPlayer(int amount)
        {
            Debug.Log($"Восстановлено {amount} здоровья");
        }

        private static Vector3 GetRandomSpawnPosition()
        {
            Vector2 direction = Random.insideUnitCircle.normalized;

            float minDistance = 5f;
            float maxDistance = 12;

            float distance = Random.Range(minDistance, maxDistance);

            return direction * distance;
        }
    }
}
#endif