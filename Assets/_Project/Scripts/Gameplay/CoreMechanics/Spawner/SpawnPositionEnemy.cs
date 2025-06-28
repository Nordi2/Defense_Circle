using Infrastructure.Services.Services.ScreenResolution;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Cor.Spawner
{
    [UsedImplicitly]
    public class SpawnPositionEnemy
    {
        private readonly IScreenResolutionService _screenResolutionService;
        private readonly float _spawnMargin;
        
        public SpawnPositionEnemy(
            float spawnMargin, 
            IScreenResolutionService screenResolutionService)
        {
            _spawnMargin = spawnMargin;
            _screenResolutionService = screenResolutionService;
        }

        private float _spawnMinXAxis => _screenResolutionService.MinMaxAxisX.x - _spawnMargin;
        private float _spawnMaxXAxis => _screenResolutionService.MinMaxAxisX.y + _spawnMargin;
        private float _spawnMinYAxis => _screenResolutionService.MinMaxAxisY.x - _spawnMargin;
        private float _spawnMaxYAxis => _screenResolutionService.MinMaxAxisY.y + _spawnMargin;

        public Vector2 GetSpawnPosition()
        {
            int side = Random.Range(0, 4);

            Vector2 spawnPos = side switch
            {
                0 => new Vector2(Random.Range(_spawnMinXAxis, _spawnMaxXAxis), _spawnMaxYAxis),
                1 => new Vector2(_spawnMaxXAxis, Random.Range(_spawnMinYAxis, _spawnMaxYAxis)),
                2 => new Vector2(Random.Range(_spawnMinXAxis, _spawnMaxXAxis), _spawnMinYAxis),
                3 => new Vector2(_spawnMinXAxis, Random.Range(_spawnMinYAxis, _spawnMaxYAxis)),
                _ => Vector2.zero
            };

            return spawnPos;
        }
    }
}