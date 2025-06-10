using System;
using _Project.Scripts.Gameplay.EnemyLogic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Spawner
{
    public class SpawnerWave : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _spawnMargin = 1.0f;

        [SerializeField] private int _maxWave;
        [SerializeField] private int _currentWave;
        [SerializeField] private EnemyFacade _enemyPrefab;

        [SerializeField] private float _spawnDelay;
        [SerializeField] private WaveSettings _waveSettings;

        private int _spawnedEnemies;
        private float _currentDurationSpawn;
        private float _currentSpawnDelay;
        private IInstantiator _instantiator;

        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;

        [Inject]
        private void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        private async void Start()
        {
            await StartWave(1);
            Debug.Log("Конец волны");
        }

        private Vector2 GetRandomSpawnPosition()
        {
            float screenAspect = (float)Screen.width / Screen.height;
            float cameraHeight = _camera.orthographicSize;

            float cameraWidth = cameraHeight * screenAspect;

            Vector2 cameraCenter = _camera.transform.position;

            _minX = cameraCenter.x - cameraWidth - _spawnMargin;
            _maxX = cameraCenter.x + cameraWidth + _spawnMargin;
            _minY = cameraCenter.y - cameraHeight - _spawnMargin;
            _maxY = cameraCenter.y + cameraHeight + _spawnMargin;

            int side = Random.Range(0, 4);

            Vector2 spawnPos = Vector2.zero;

            switch (side)
            {
                case 0:
                    spawnPos = new Vector2(Random.Range(_minX, _maxX), _maxY);
                    break;
                case 1:
                    spawnPos = new Vector2(_maxX, Random.Range(_minY, _maxY));
                    break;
                case 2:
                    spawnPos = new Vector2(Random.Range(_minX, _maxX), _minY);
                    break;
                case 3:
                    spawnPos = new Vector2(_minX, Random.Range(_minY, _maxY));
                    break;
            }

            return spawnPos;
        }

        private async UniTask StartWave(int currentWave)
        {
            while (_currentDurationSpawn <= _waveSettings._durationWave[currentWave])
            {
                _currentDurationSpawn += Time.deltaTime;
                _currentSpawnDelay += Time.deltaTime;

                if (_currentSpawnDelay >= _spawnDelay)
                {
                    int randomCountSpawn = Random.Range(1, _waveSettings._maxEnemyWave[currentWave]);

                    Debug.Log(randomCountSpawn);
                    _spawnedEnemies = Math.Clamp(_spawnedEnemies + randomCountSpawn, 1,
                        _waveSettings._maxEnemyWave[currentWave] - _spawnedEnemies);
                    
                    _currentSpawnDelay = 0;

                    if (_spawnedEnemies <= _waveSettings._maxEnemyWave[currentWave])
                    {
                        for (int i = 0; i < randomCountSpawn; i++)
                        {
                            Debug.Log("SpawnEnemy");
                            _instantiator.InstantiatePrefab(_enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity,
                                null);
                        }
                    }
                }

                await UniTask.NextFrame();
            }
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(_minX, _minY), new Vector3(_maxX, _minY));
            Gizmos.DrawLine(new Vector3(_minX, _minY), new Vector3(_minX, _maxY));

            Gizmos.DrawLine(new Vector3(_maxX, _maxY), new Vector3(_maxX, _minY));
            Gizmos.DrawLine(new Vector3(_maxX, _maxY), new Vector3(_minX, _maxY));
        }
#endif
    }

    [Serializable]
    class WaveSettings
    {
        [SerializeField] public int[] _maxEnemyWave;
        [SerializeField] public float[] _durationWave;
    }
}