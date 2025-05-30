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

        [Inject]
        private void Construct(Tower tower)
        {
            _tower = tower;
        }

        public void SpawnEnemy(EnemyType type)
        {
            Vector3 randomPosition = GetRandomSpawnPosition();
            
            switch (type)
            {
                case EnemyType.Default:
                    Instantiate(EnemyDefaultPrefab, randomPosition, Quaternion.identity);
                    break;
                case EnemyType.Fast:
                    Instantiate(EnemyFastPrefab, randomPosition, Quaternion.identity);
                    break;
                case EnemyType.Slow:
                    Instantiate(EnemySlowPrefab,randomPosition, Quaternion.identity);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        public void AddMoney(int amount)
        {
            Debug.Log($"Добавлено {amount} денег");
        }

        public void TakeDamage(int amount)
        {
            _tower.TakeDamage(amount);
            D.Log(GetType().Name.ToUpper(),$"Take Damage: {amount}",DColor.AQUAMARINE,true);
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