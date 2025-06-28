using _Project.Cor.Enemy;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Project.Data.Config
{
    [System.Serializable]
    public class WaveSettings
    {
        [field: SerializeField] public Vector2Int MinMaxGroupEnemy { get;private set; }
        [field: SerializeField] public int MaxEnemiesAtOnce { get; private set; }
        [field: SerializeField] public float DurationWave { get; private set; }
        [field: SerializeField] public float SpawnInterval { get; private set; }
        [field: SerializeField] public SerializedDictionary<EnemyType, float> EnemyInWaveInterest { get; private set; }
    }
}