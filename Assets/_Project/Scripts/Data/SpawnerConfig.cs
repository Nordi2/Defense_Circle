using UnityEngine;

namespace _Project.Data.Config
{
    [CreateAssetMenu(
        fileName = "SpawnerConfig",
        menuName = "Configs/Spawner")]
    public class SpawnerConfig : ScriptableObject
    {
        [field: SerializeField] public float SpawnMargin { get; private set; }
        [field: SerializeField] public int InitialWave { get; private set; }
        [field: SerializeField, Min(1)] public int MaxWave { get; private set; }

        [SerializeField] private WaveSettings[] _waveSettings;

        private void OnValidate()
        {
            if (_waveSettings.Length != MaxWave)
                _waveSettings = new WaveSettings[MaxWave];
        }

        public WaveSettings GetWaveSettings(int waveNumber) =>
            _waveSettings[waveNumber];
    }
}