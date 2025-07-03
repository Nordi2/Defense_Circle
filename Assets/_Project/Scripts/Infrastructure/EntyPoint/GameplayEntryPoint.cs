using _Project.Infrastructure.Services;
using _Project.Meta.StatsLogic;
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
        private readonly ICreateStatsService _createStatsService;
        private readonly ICheatManagerService _initializerCheatManager;
        
        public GameplayEntryPoint(
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            ICreateStatsService createStatsService,
            ICheatManagerService initializerCheatManager)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _createStatsService = createStatsService;
            _initializerCheatManager = initializerCheatManager;
        }

        void IInitializable.Initialize()
        {
            _createStatsService.CreateStats();

            InitialTextLoadAfterLoading initialText = _uiFactory.CreateInitialTextLoadAfterLoading();

            _gameFactory.CreateGameplayVolume();
            _gameFactory.CreateBackground();
            _gameFactory.CreateBackgroundEffect();

            _uiFactory.CreateMenu();
            _uiFactory.CreateShop();

            _gameFactory.CreateTower();

            initialText.StartAnimation();
            
            _initializerCheatManager.Init();
        }
    }
}