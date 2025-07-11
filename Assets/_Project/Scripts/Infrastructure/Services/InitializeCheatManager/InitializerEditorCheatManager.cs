#if UNITY_EDITOR
using System;
using _Project;
using _Project.Cor.Enemy;
using _Project.Infrastructure.Services;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using Infrastructure.Services;
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
        private readonly UIFactory _uiFactory;
        private readonly GameFactory _gameFactory;
        
        public InitializerEditorCheatManager(
            SignalBus signalBus,
            StatsStorage statsStorage,
            Wallet wallet,
            GameFactory gameFactory,
            UIFactory uiFactory)
        {
            _signalBus = signalBus;
            _statsStorage = statsStorage;
            _wallet = wallet;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        void IInitializable.Initialize() =>
            _signalBus.Subscribe<StartGameSignal>(OnStartGame);

        void IDisposable.Dispose() =>
            _signalBus.Unsubscribe<StartGameSignal>(OnStartGame);

        public void Init()
        {
            CheatManager.TowerFacade = _gameFactory.TowerFacade;
            CheatManager.WaveSpawner = _gameFactory.WaveSpawner;
            CheatManager.ShopPresenter = _uiFactory.ShopPresenter;
            CheatManager.StatsStorage = _statsStorage;
            CheatManager.Wallet = _wallet;
            CheatManager.GameFactory = _gameFactory;
        }

        private void OnStartGame()
        {
            CheatManager.ActivateCheats = true;
        }
    }

#endif