using _Project.Cor.Spawner;
using _Project.Infrastructure.Services;
using JetBrains.Annotations;
using Zenject;

namespace _Project.Infrastructure.EntryPoint
{
    [UsedImplicitly]
    public class GameplayEntryPoint :
        IInitializable
    {
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly ICheatManagerService _initializerCheatManager;

        public GameplayEntryPoint(
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            ICheatManagerService initializerCheatManager)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _initializerCheatManager = initializerCheatManager;
        }

        void IInitializable.Initialize()
        {
            InitialTextLoadAfterLoading initialText = _uiFactory.CreateInitialTextLoadAfterLoading();

            _gameFactory.CreateStats();
            _gameFactory.CreateGameplayVolume();
            _gameFactory.CreateBackground();
            _gameFactory.CreateBackgroundEffect();

            _gameFactory.CreateTower();
            WaveSpawner spawner = _gameFactory.CreateSpawner();

            _uiFactory.CreateShop(spawner);
            
            initialText.StartAnimation();

            _initializerCheatManager.Init();
        }
    }
}