using _Project.Infrastructure.Services;
using _Project.Meta.StatsLogic;
using Infrastructure.Services.Services.InitializeCheatManager;
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

        private readonly InitializeCheatManagerService _cheatManagerService;

        public GameplayEntryPoint(
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            ICreateStatsService createStatsService,
            InitializeCheatManagerService cheatManagerService)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _createStatsService = createStatsService;
            _cheatManagerService = cheatManagerService;
        }

        void IInitializable.Initialize()
        {
            _createStatsService.CreateStats();

            InitialTextLoadAfterLoading initialText = _uiFactory.CreateInitialTextLoadAfterLoading();
            
            _gameFactory.CreateBackground();
            _gameFactory.CreateBackgroundEffect();
            _uiFactory.CreateMenu();
            _uiFactory.CreateShop();
            _gameFactory.CreateTower();

            initialText.StartAnimation();

#if UNITY_EDITOR

            _cheatManagerService.Init();
#endif
        }
    }
}