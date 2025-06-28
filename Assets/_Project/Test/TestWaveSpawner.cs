/*using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;

public class TestWaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyType> enemyTypes;
        public float spawnInterval = 2f;
        public int maxEnemiesAtOnce = 10;
        public int totalEnemiesToSpawn = 20;
        public float waveDuration = 30f;
    }
    
    [SerializeField] private List<Wave> waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject buttonNextWave;
    
    private Dictionary<EnemyType, GameObject> _enemyPrefabs;
    private int _currentWaveIndex = 0;
    private int _enemiesSpawnedThisWave = 0;
    private int _enemiesAlive = 0;
    private bool _isWaveActive = false;
    private bool _isSpawning = false;
    private float _waveTimer = 0f;
    private CancellationTokenSource _waveCts;

    private void Start()
    {
        buttonNextWave.SetActive(true);
        LoadEnemyPrefabs();
    }

    private void OnDestroy()
    {
        _waveCts?.Cancel();
        _waveCts?.Dispose();
    }
    
    private void LoadEnemyPrefabs()
    {
        _enemyPrefabs = new Dictionary<EnemyType, GameObject>();
        
        foreach (EnemyType type in System.Enum.GetValues(typeof(EnemyType)))
        {
            GameObject prefab = Resources.Load<GameObject>($"Enemies/{type}");
            if (prefab != null)
            {
                _enemyPrefabs.Add(type, prefab);
            }
            else
            {
                Debug.LogError($"Prefab for {type} not found in Resources/Enemies!");
            }
        }
    }
    
    public void StartNextWave()
    {
        if (_isWaveActive || _currentWaveIndex >= waves.Count) return;

        buttonNextWave.SetActive(false);
        StartWaveAsync().Forget();
    }
    
    private async UniTaskVoid StartWaveAsync()
    {
        _waveCts?.Cancel();
        _waveCts = new CancellationTokenSource();
        
        await WaveRoutine(waves[_currentWaveIndex], _waveCts.Token);
    }
    
    private async UniTask WaveRoutine(Wave wave, CancellationToken ct)
    {
        _isWaveActive = true;
        _isSpawning = true;
        _enemiesSpawnedThisWave = 0;
        _waveTimer = wave.waveDuration;

        Debug.Log($"Starting wave: {wave.waveName}");

        // Запускаем параллельные задачи
        var waveTimerTask = WaveTimer(ct);
        var spawnEnemiesTask = SpawnEnemies(wave, ct);

        // Ждем завершения обеих задач
        await UniTask.WhenAll(waveTimerTask, spawnEnemiesTask);

        Debug.Log($"Wave {wave.waveName} completed!");
        _currentWaveIndex++;

        if (_currentWaveIndex < waves.Count)
        {
            buttonNextWave.SetActive(true);
        }
        else
        {
            Debug.Log("All waves completed!");
        }
    }
    
    private async UniTask WaveTimer(CancellationToken ct)
    {
        while (_waveTimer > 0 && _isWaveActive && !ct.IsCancellationRequested)
        {
            _waveTimer -= Time.deltaTime;
            await UniTask.Yield(PlayerLoopTiming.Update, ct);
        }

        _isSpawning = false;
        Debug.Log("Wave time expired! Stopping spawns.");
    }

    private async UniTask SpawnEnemies(Wave wave, CancellationToken ct)
    {
        while (_isSpawning && 
               _enemiesSpawnedThisWave < wave.totalEnemiesToSpawn && 
               !ct.IsCancellationRequested)
        {
            if (_enemiesAlive < wave.maxEnemiesAtOnce)
            {
                SpawnEnemy(wave);
                await UniTask.Delay((int)(wave.spawnInterval * 1000), false, PlayerLoopTiming.Update, ct);
            }
            else
            {
                await UniTask.Yield(PlayerLoopTiming.Update, ct);
            }
        }

        // Ждем пока все враги будут убиты
        while (_enemiesAlive > 0 && !ct.IsCancellationRequested)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, ct);
        }

        _isWaveActive = false;
    }

    private void SpawnEnemy(Wave wave)
    {
        if (spawnPoints.Length == 0 || wave.enemyTypes.Count == 0) return;

        EnemyType randomType = wave.enemyTypes[Random.Range(0, wave.enemyTypes.Count)];

        if (_enemyPrefabs.TryGetValue(randomType, out GameObject prefab))
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            
            if (enemy.TryGetComponent<EnemyHealth>(out var health))
            {
                health.OnDeath += OnEnemyDeath;
            }

            _enemiesSpawnedThisWave++;
            _enemiesAlive++;
        }
    }

    private void OnEnemyDeath()
    {
        _enemiesAlive--;
        Debug.Log($"Enemy died. Alive: {_enemiesAlive}");
    }

    private void OnGUI()
    {
        if (_isWaveActive)
        {
            GUILayout.Label($"Current Wave: {waves[_currentWaveIndex].waveName}");
            GUILayout.Label($"Time Left: {Mathf.Ceil(_waveTimer)}s");
            GUILayout.Label($"Enemies Spawned: {_enemiesSpawnedThisWave}/{waves[_currentWaveIndex].totalEnemiesToSpawn}");
            GUILayout.Label($"Enemies Alive: {_enemiesAlive}");
        }
    }
}*/