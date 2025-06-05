using System;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
using _Project.Scripts.Infrastructure.Signals;
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
        private readonly SignalBus _signalBus;
        private readonly CompositeDisposable _disposable;
        private readonly IInputService _inputService;
        private readonly InitialTextLoadAfterLoading _initialText;
        private readonly UIRoot _uiRoot;

        public GameplayBootstrapper(
            InitialTextLoadAfterLoading initialText,
            UIRoot uiRoot,
            IInputService inputService,
            CompositeDisposable disposable,
            SignalBus signalBus)
        {
            _disposable = disposable;
            _signalBus = signalBus;
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

        void IDisposable.Dispose() =>
            _disposable.Dispose();

        private void RunGame(Unit unit)
        {
            _signalBus.Fire(new StartGameSignal());
        }
    }
}