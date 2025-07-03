#if UNITY_EDITOR
using System;
using _Project;
using _Project.Cor.Spawner;
using _Project.Infrastructure.Services;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using Infrastructure.Signals;
using JetBrains.Annotations;
using Zenject;

    [UsedImplicitly]
    public class InitializerEditorCheatManager :
        IInitializable,
        IDisposable, 
        ICheatManagerService
    {
        private readonly SignalBus _signalBus;
        private readonly StatsStorage _statsStorage;
        private readonly Wallet _wallet;
        private readonly WaveSpawner _spawner;
        private readonly UIFactory _uiFactory;
        private readonly GameFactory _gameFactory;

        public InitializerEditorCheatManager(
            SignalBus signalBus,
            StatsStorage statsStorage,
            Wallet wallet,
            GameFactory gameFactory,
            WaveSpawner spawner,
            UIFactory uiFactory)
        {
            _signalBus = signalBus;
            _statsStorage = statsStorage;
            _wallet = wallet;
            _gameFactory = gameFactory;
            _spawner = spawner;
            _uiFactory = uiFactory;
        }

        void IInitializable.Initialize() =>
            _signalBus.Subscribe<StartGameSignal>(OnStartGame);

        void IDisposable.Dispose() =>
            _signalBus.Unsubscribe<StartGameSignal>(OnStartGame);

        public void Init()
        {
            CheatManager.TowerFacade = _gameFactory.TowerFacade;
            CheatManager.ShopPresenter = _uiFactory.ShopPresenter;
            CheatManager.PresenterMainMenu = _uiFactory.MenuPresenter;
            CheatManager.StatsStorage = _statsStorage;
            CheatManager.Wallet = _wallet;
            CheatManager.GameFactory = _gameFactory;
            CheatManager.WaveSpawner = _spawner;
        }

        private void OnStartGame()
        {
            CheatManager.ActivateCheats = true;
        }
    }

#endif