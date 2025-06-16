using System;
using _Project.Infrastructure.Services;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Test;
using _Project.Scripts.UI;
using Cor.Tower.Mono;
using Infrastructure.Services;
using Infrastructure.Signals;
using JetBrains.Annotations;
using Meta.Money;
using R3;
using Zenject;

namespace _Project.Infrastructure.EntryPoint
{
    [UsedImplicitly]
    public class GameplayEntryPoint :
        IInitializable,
        IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly CompositeDisposable _disposable;
        private readonly IInputService _inputService;
        private readonly InitialTextLoadAfterLoading _initialText;
        private readonly UIRoot _uiRoot;
        private readonly IGameFactory _gameFactory;
        private readonly DiContainer _container;
        private readonly ShopUpgrade _shopUpgrade;
        private SpawnerTest _spawnerTest;

        public GameplayEntryPoint(
            InitialTextLoadAfterLoading initialText,
            UIRoot uiRoot,
            IInputService inputService,
            CompositeDisposable disposable,
            SignalBus signalBus,
            IGameFactory gameFactory,
            DiContainer container,
            SpawnerTest spawnerTest,
            ShopUpgrade shopUpgrade)
        {
            _disposable = disposable;
            _signalBus = signalBus;
            _gameFactory = gameFactory;
            _container = container;
            _spawnerTest = spawnerTest;
            _shopUpgrade = shopUpgrade;
            _initialText = initialText;
            _uiRoot = uiRoot;
            _inputService = inputService;
        }

        void IInitializable.Initialize()
        {
            _inputService
                .OnClickSpaceButton
                .Subscribe(RunGame)
                .AddTo(_disposable);

            _shopUpgrade.Hide(true);
            _uiRoot.AddToContainer(_initialText.RectTransform);
            _uiRoot.AddToContainer(_shopUpgrade.RectTransform);
            _initialText.StartAnimation();
        }

        void IDisposable.Dispose() =>
            _disposable.Dispose();

        private void RunGame(Unit unit)
        {
            TowerFacade tower = _gameFactory.CreateTower();

            _signalBus.Fire(new StartGameSignal());

            // _spawnerTest.StartSpawn();

#if UNITY_EDITOR
            CheatManager.TowerFacade = tower;
            CheatManager.EnemyPool = _container.Resolve<EnemyPool>();
            CheatManager.Wallet = _container.Resolve<Wallet>();
            CheatManager.ShopUpgrade = _shopUpgrade;
#endif
        }
    }
}