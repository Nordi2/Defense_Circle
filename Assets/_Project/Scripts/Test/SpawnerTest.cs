using System;
using _Project.Cor.Enemy.Mono;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Test
{
    public class SpawnerTest
    {
        public event Action OnEndWave;
        
        private readonly int _countEnemyInWave = 4;
        private readonly IInstantiator _instantiator;
        private readonly EnemyFacade _enemyPrefab;

        public SpawnerTest(IInstantiator instantiator, EnemyFacade enemyPrefab)
        {
            _instantiator = instantiator;
            _enemyPrefab = enemyPrefab;
        }

        public async UniTask StartSpawn()
        {
            Debug.Log("Start Wave:");

            for (int i = 0; i < _countEnemyInWave; i++)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1));

                _instantiator.InstantiatePrefab(_enemyPrefab,
                    new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity, null);
            }
            
            OnEndWave?.Invoke();
            Debug.Log("End Wave:");
        }
    }
}