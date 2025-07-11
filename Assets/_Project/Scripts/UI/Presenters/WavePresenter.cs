using _Project.Cor.Spawner;
using Infrastructure.Services;

namespace _Project.Scripts.Gameplay.Component
{
    public class WavePresenter :
        IGameStartListener
    {
        private readonly WaveSpawner _spawner;
        private readonly WaveView _view;

        public WavePresenter(
            WaveView view,
            WaveSpawner spawner)
        {
            _view = view;
            _spawner = spawner;
        }
        
        void IGameStartListener.OnGameStart()
        {
            StartWave();
            _view.UpdateMaxNumberWave(_spawner.MaxWave.ToString());
        }

        public void StartWave()
        {
            _view.StartTimerToNextWaveAnimation(_spawner.StartSpawn);
            _view.UpdateCurrentNumberWave(_spawner.CurrentWave.ToString());
        }
    }
}