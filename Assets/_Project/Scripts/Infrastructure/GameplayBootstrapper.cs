using System;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
using _Project.Scripts.UI;
using DG.Tweening;
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
        private readonly CompositeDisposable _disposable;
        private readonly GameLoopService _gameLoopService;
        private readonly IInputService _inputService;
        private readonly InitialTextLoadAfterLoading _initialText;
        private readonly UIRoot _uiRoot;

        public GameplayBootstrapper(
            InitialTextLoadAfterLoading initialText,
            UIRoot uiRoot,
            IInputService inputService,
            GameLoopService gameLoopService,
            CompositeDisposable disposable)
        {
            _gameLoopService = gameLoopService;
            _disposable = disposable;
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