#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using _Project.Cor.Enemy;
using _Project.Cor.Enemy.Mono;
using _Project.Cor.Spawner;
using _Project.Cor.Tower.Mono;
using _Project.Infrastructure.Services;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Shop;
using DebugToolsPlus;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project
{
    [UsedImplicitly]
    public static class CheatManager
    {
        private const string CheathManager = "CheathManager";

        public static bool ActivateCheats;
        public static IGameFactory GameFactory;
        public static TowerFacade TowerFacade;
        public static Wallet Wallet;
        public static StatStorage StatStorage;
        public static ShopPresenter ShopPresenter;
        public static WaveSpawner WaveSpawner;

        private static readonly List<EnemyFacade> _enemiesInSpawned = new();

        public static void Initialize(
            WaveSpawner waveSpawner,
            TowerFacade towerFacade,
            Wallet wallet,
            StatStorage statStorage,
            ShopPresenter shopPresenter,
            GameFactory factory)
        {
            GameFactory  = factory;
            TowerFacade = towerFacade;
            Wallet = wallet;
            StatStorage = statStorage;
            ShopPresenter = shopPresenter;
            WaveSpawner = waveSpawner;
        }

        public static void SpawnEnemy(EnemyType type)
        {
            Vector3 randomPosition = GetRandomSpawnPosition();

            switch (type)
            {
                case EnemyType.Default:
                    CreateEnemyAndGetLog(randomPosition, "Create: Default-Enemy\n", EnemyType.Default);
                    break;
                case EnemyType.Fast:
                    CreateEnemyAndGetLog(randomPosition, "Create: Fast-Enemy\n", EnemyType.Fast);
                    break;
                case EnemyType.Slow:
                    CreateEnemyAndGetLog(randomPosition, "Create: Slow-Enemy\n", EnemyType.Slow);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static void CreateEnemyAndGetLog(Vector3 randomPosition, string log, EnemyType type)
        {
            EnemyFacade enemyFacade = GameFactory.CreateEnemy(type);
            enemyFacade.transform.position = randomPosition;
            enemyFacade.OnDeath += DeleteFromList;
            _enemiesInSpawned.Add(enemyFacade);

            // D.Log(CheathManager.ToUpper(), log + D.FormatText(enemyFacade.ShowStatsService.ToString(), DColor.RED),
            //     enemyFacade.gameObject, DColor.AQUAMARINE, true);
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
        
        public static void OpenShop()
        {
            D.Log(CheathManager.ToUpper(), "Open Shop", DColor.AQUAMARINE, true);
            ShopPresenter.OpenShop();
        }

        public static void CloseShop()
        {
            D.Log(CheathManager.ToUpper(), "Close Shop", DColor.AQUAMARINE, true);
            ShopPresenter.HideShop();
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
            D.Log(CheathManager.ToUpper(), $"Recover Tower: {amount}", DColor.AQUAMARINE, true);
            TowerFacade.RecoverTower(amount);
        }

        private static Vector3 GetRandomSpawnPosition()
        {
            Vector2 direction = Random.insideUnitCircle.normalized;

            float minDistance = 5f;
            float maxDistance = 12;

            float distance = Random.Range(minDistance, maxDistance);

            return direction * distance;
        }

        public static void StaticZero()
        {
            ActivateCheats = false;
            TowerFacade = null;
            Wallet = null;
            StatStorage = null;
            ShopPresenter = null;
            GameFactory = null;
            WaveSpawner = null;
        }
    }
}
#endif