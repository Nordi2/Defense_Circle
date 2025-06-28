#if UNITY_EDITOR

using System;
using _Project;
using _Project.Cor.Spawner;
using _Project.Cor.Tower.Mono;
using _Project.Infrastructure.Services;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using _Project.Scripts.UI.Shop;
using Infrastructure.Signals;
using JetBrains.Annotations;
using Zenject;

namespace Infrastructure.Services.Services.InitializeCheatManager
{
    [UsedImplicitly]
    public class InitializeCheatManagerService :
        IInitializable,
        IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly StatsStorage _statsStorage;
        private readonly Wallet _wallet;
        private readonly IGameFactory _gameFactory;
        private readonly NewSpawnerWave _spawnerWave;
        
        public InitializeCheatManagerService(
            SignalBus signalBus,
            StatsStorage statsStorage,
            Wallet wallet,
            IGameFactory gameFactory,
            NewSpawnerWave spawnerWave)
        {
            _signalBus = signalBus;
            _statsStorage = statsStorage;
            _wallet = wallet;
            _gameFactory = gameFactory;
            _spawnerWave = spawnerWave;
        }

        void IInitializable.Initialize() =>
            _signalBus.Subscribe<StartGameSignal>(OnStartGame);

        void IDisposable.Dispose() =>
            _signalBus.Unsubscribe<StartGameSignal>(OnStartGame);

        public void Init(TowerFacade tower, ShopPresenter presenter)
        {
            CheatManager.TowerFacade = tower;

            CheatManager.ShopPresenter = presenter;
            CheatManager.StatsStorage = _statsStorage;
            CheatManager.Wallet = _wallet;
            CheatManager.GameFactory = _gameFactory;
            CheatManager.WaveSpawner = _spawnerWave;
        }

        private void OnStartGame()
        {
            CheatManager.ActivateCheats = true;
        }
    }
}
#endif