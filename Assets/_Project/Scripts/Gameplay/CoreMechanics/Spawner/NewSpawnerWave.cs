using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Project.Cor.Enemy;
using _Project.Cor.Enemy.Mono;
using _Project.Data.Config;
using _Project.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Cor.Spawner
{
    [UsedImplicitly]
    public class NewSpawnerWave
    {
        private readonly SpawnPositionEnemy _spawnPositionEnemy;
        private readonly SpawnerConfig _spawnerConfig;
        private readonly IGameFactory _gameFactory;

        private int _currentWave;
        private int _enemiesAlive;
        private bool _isWaveActive;
        private bool _isSpawning;
        private float _waveTimer;
        private CancellationTokenSource _waveCts;

        private float _totalChance;
        private Dictionary<EnemyType,float> _dictionarySpawnChance;
        
        public NewSpawnerWave(
            SpawnPositionEnemy spawnPositionEnemy,
            SpawnerConfig spawnerConfig,
            IGameFactory gameFactory)
        {
            _spawnPositionEnemy = spawnPositionEnemy;
            _spawnerConfig = spawnerConfig;
            _gameFactory = gameFactory;

            _currentWave = spawnerConfig.InitialWave;
        }

        public void StartWave()
        {
            if (_isWaveActive || _currentWave >= _spawnerConfig.MaxWave)
                return;

            StartWaveAsync().Forget();
        }

        private async UniTaskVoid StartWaveAsync()
        {
            _waveCts?.Cancel();
            _waveCts = new CancellationTokenSource();

            await WaveAsync(_spawnerConfig.GetWaveSettings(_currentWave), _waveCts.Token);
        }

        private async UniTask WaveAsync(WaveSettings waveSettings, CancellationToken token)
        {
            UpdateData(waveSettings);

            UniTask waveTimerTask = WaveTimerAsync(token);
            UniTask spawnEnemiesTask = SpawnEnemiesAsync(waveSettings, token);

            await UniTask.WhenAll(spawnEnemiesTask, waveTimerTask);
            
            _currentWave++;
        }

        private async UniTask SpawnEnemiesAsync(WaveSettings waveSettings, CancellationToken token)
        {
            while (_isSpawning &&
                   !token.IsCancellationRequested)
            {
                if (_enemiesAlive < waveSettings.MaxEnemiesAtOnce)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(waveSettings.SpawnInterval), false,
                        PlayerLoopTiming.Update, token);
                    SpawnEnemy(waveSettings);
                }
                else
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, token);
                }
            }

            while (_enemiesAlive > 0 && !token.IsCancellationRequested)
                await UniTask.Yield(PlayerLoopTiming.Update, token);

            _isWaveActive = false;
        }

        private async UniTask WaveTimerAsync(CancellationToken token)
        {
            while (_waveTimer > 0 && !token.IsCancellationRequested)
            {
                _waveTimer -= Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }

            _isSpawning = false;
        }

        private void SpawnEnemy(WaveSettings waveSettings)
        {
            EnemyType typeSpawn = GetRandomEnemyByChance(waveSettings);
            
            int groupEnemy = Random.Range(waveSettings.MinMaxGroupEnemy.x, waveSettings.MinMaxGroupEnemy.y);
            int difference = Mathf.Abs(_enemiesAlive - waveSettings.MaxEnemiesAtOnce);
            
            groupEnemy = Mathf.Clamp(groupEnemy, 1, difference);

            for (int i = 0; i < groupEnemy; i++)
            {
                EnemyFacade enemy = _gameFactory.CreateEnemy(typeSpawn);
                enemy.transform.position = _spawnPositionEnemy.GetSpawnPosition();
                enemy.OnDeath += EnemyDeath;
            }

            _enemiesAlive += groupEnemy;
        }

        private EnemyType GetRandomEnemyByChance(WaveSettings waveSettings)
        {
            float randomPoint = Random.Range(0f, _totalChance);
            float current = 0f;

            foreach (KeyValuePair<EnemyType,float> kvp in _dictionarySpawnChance)
            {
                current += kvp.Value;
                if(randomPoint <= current)
                    return kvp.Key;
            }

            return waveSettings.EnemyInWaveInterest.Keys.First();
        }

        private void EnemyDeath(EnemyFacade enemy)
        {
            enemy.OnDeath -= EnemyDeath;
            _enemiesAlive--;
        }

        private void UpdateData(WaveSettings waveSettings)
        {
            _isWaveActive = true;
            _isSpawning = true;
            _waveTimer = waveSettings.DurationWave;
            _dictionarySpawnChance = waveSettings.EnemyInWaveInterest;
            _totalChance = waveSettings.EnemyInWaveInterest.Values.Sum();
        }
    }
}