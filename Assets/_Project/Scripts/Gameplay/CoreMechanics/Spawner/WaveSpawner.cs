using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Project.Cor.Enemy;
using _Project.Cor.Enemy.Mono;
using _Project.Data.Config;
using Cysharp.Threading.Tasks;
using Infrastructure.Services;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Cor.Spawner
{
    [UsedImplicitly]
    public class WaveSpawner :
        IStartWaveEvent,
        IEndWaveEvent
    {
        private SpawnPositionEnemy _spawnPositionEnemy;
        private SpawnerConfig _spawnerConfig;

        [Inject(Id = EnemyType.Default)] private EnemyPool _enemyDefaultPool;
        [Inject(Id = EnemyType.Fast)] private EnemyPool _enemyFastPool;
        [Inject(Id = EnemyType.Slow)] private EnemyPool _enemySlowPool;

        private int _enemiesAlive;
        private float _waveTimer;
        private bool _isWaveActive;
        private bool _isSpawning;
        private CancellationTokenSource _waveCts;

        private float _totalChance;
        private Dictionary<EnemyType, float> _dictionarySpawnChance;
        
        public void Init(
            SpawnPositionEnemy spawnPositionEnemy,
            SpawnerConfig spawnerConfig)
        {
            _spawnPositionEnemy = spawnPositionEnemy;
            _spawnerConfig = spawnerConfig;

            CurrentWave = spawnerConfig.InitialWave;
        }

        private bool _isMaxWave => CurrentWave >= _spawnerConfig.MaxWave;

        public int MaxWave => _spawnerConfig.MaxWave;
        public Subject<Unit> OnEndWave { get; } = new();
        public Subject<Unit> OnStartWave { get; } = new();
        public int CurrentWave { get; private set; }

        public void StartSpawn()
        {
            if (_isWaveActive || _isMaxWave)
                return;

            StartSpawnAsync().Forget();

            OnStartWave?.OnNext(Unit.Default);
        }

        private async UniTaskVoid StartSpawnAsync()
        {
            _waveCts?.Cancel();
            _waveCts = new CancellationTokenSource();

            await WaveAsync(_spawnerConfig.GetWaveSettings(CurrentWave), _waveCts.Token);

            OnEndWave?.OnNext(Unit.Default);
        }

        private async UniTask WaveAsync(WaveSettings waveSettings, CancellationToken token)
        {
            UpdateData(waveSettings);

            UniTask waveTimerTask = WaveTimerAsync(token);
            UniTask spawnEnemiesTask = SpawnEnemiesAsync(waveSettings, token);

            await UniTask.WhenAll(spawnEnemiesTask, waveTimerTask);

            CurrentWave++;
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
                EnemyFacade enemy = _enemyDefaultPool.Spawn(_spawnPositionEnemy.GetSpawnPosition());
                enemy.OnDeath += EnemyDeath;
            }

            _enemiesAlive += groupEnemy;
        }

        private EnemyType GetRandomEnemyByChance(WaveSettings waveSettings)
        {
            float randomPoint = Random.Range(0f, _totalChance);
            float current = 0f;

            foreach (KeyValuePair<EnemyType, float> kvp in _dictionarySpawnChance)
            {
                current += kvp.Value;
                if (randomPoint <= current)
                    return kvp.Key;
            }

            return waveSettings.EnemyInWaveInterest.Keys.First();
        }

        private void EnemyDeath(EnemyFacade enemy)
        {
            _enemyDefaultPool.Despawn(enemy);
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