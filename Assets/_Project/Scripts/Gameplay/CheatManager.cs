using System;
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
        [Header("Spawn Settings")] 
        public Enemy EnemyFastPrefab;
        public Enemy EnemyDefaultPrefab;
        public Enemy EnemySlowPrefab;

        private Tower _tower;
        private IInstantiator _instantiator;
        
        [Inject]
        private void Construct(
            Tower tower,
            IInstantiator instantiator)
        {
            _tower = tower;
            _instantiator = instantiator;
        }

        public void SpawnEnemy(EnemyType type)
        {
            Vector3 randomPosition = GetRandomSpawnPosition();
            
            switch (type)
            {
                case EnemyType.Default:
                    CreateEnemyAndGetLog(randomPosition,"Create: Default-Enemy",EnemyDefaultPrefab);
                    break;
                case EnemyType.Fast:
                    CreateEnemyAndGetLog(randomPosition, "Create: Fast-Enemy",EnemyFastPrefab);
                    break;
                case EnemyType.Slow:
                    CreateEnemyAndGetLog(randomPosition,"Create: Slow-Enemy",EnemySlowPrefab);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void CreateEnemyAndGetLog(Vector3 randomPosition,string log,Enemy prefab)
        {
            D.Log(GetType().Name.ToUpper(),log ,DColor.AQUAMARINE,true);
            _instantiator.InstantiatePrefab(prefab, randomPosition, Quaternion.identity, null);
        }

        public void AddMoney(int amount)
        {
            Debug.Log($"Добавлено {amount} денег");
        }

        public void TakeDamage(int amount)
        {
            D.Log(GetType().Name.ToUpper(),$"Take Damage: {amount}",DColor.AQUAMARINE,true);
            _tower.TakeDamage(amount);
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