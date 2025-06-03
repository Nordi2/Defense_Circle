using System;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.TowerLogic;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
using _Project.Scripts.UI;
using JetBrains.Annotations;
using R3;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    [UsedImplicitly]
    public class GameplayBootstrapper :
        IInitializable,
        IDisposable
    {
        private IDisposable _disposable;

        private readonly GameLoopService _gameLoopService;
        private readonly IInputService _inputService;
        private readonly InitialTextLoadAfterLoading _initialText;
        private readonly UIRoot _uiRoot;

        public GameplayBootstrapper(
            InitialTextLoadAfterLoading initialText,
            UIRoot uiRoot,
            IInputService inputService,
            GameLoopService gameLoopService)
        {
            _gameLoopService = gameLoopService;
            _initialText = initialText;
            _uiRoot = uiRoot;
            _inputService = inputService;
        }

        void IInitializable.Initialize()
        {
            _disposable = _inputService
                .OnClickSpaceButton
                .Subscribe(RunGame);

            _uiRoot.AddToContainer(_initialText.RectTransform);
            _initialText.StartAnimation();
        }

        private void RunGame(Unit unit)
        {
            _gameLoopService.StartGame();
            
            _initialText.StopAnimation();
        }

        void IDisposable.Dispose() =>
            _disposable.Dispose();
    }
}