using System;
using System.Collections.Generic;
using Infrastructure.Signals;
using JetBrains.Annotations;
using Zenject;

namespace Infrastructure.Services
{
    [UsedImplicitly]
    public class GameLoopService :
        IInitializable,
        IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly IGameListener[] _gameListeners;

        private readonly List<IGameStartListener> _gameStartListeners;
        private readonly List<IGameFinishListener> _gameFinishListeners;
        private readonly List<IGamePauseListener> _gamePauseListeners;
        private readonly List<IGameResumeListener> _gameResumeListeners;
        private readonly List<IDisposable> _gameDisposables;
        private readonly List<IUpdatable> _gameUpdatables;

        public GameLoopService(IGameListener[] gameListeners, SignalBus signalBus)
        {
            _gameListeners = gameListeners;
            _signalBus = signalBus;

            _gameStartListeners = new List<IGameStartListener>();
            _gameFinishListeners = new List<IGameFinishListener>();
            _gamePauseListeners = new List<IGamePauseListener>();
            _gameResumeListeners = new List<IGameResumeListener>();
            _gameUpdatables = new List<IUpdatable>();
            _gameDisposables = new List<IDisposable>();
        }

        public void AddGameListener(IGameListener listener)
        {
            if (listener is IGameStartListener gameStartListener)
                _gameStartListeners.Add(gameStartListener);
        }

        public void AddDisposable(IDisposable disposable) =>
            _gameDisposables.Add(disposable);

        public void AddUpdatable(IUpdatable listener) =>
            _gameUpdatables.Add(listener);

        void IInitializable.Initialize()
        {
            _signalBus.Subscribe<StartGameSignal>(StartGame);
            _signalBus.Subscribe<FinishGameSignal>(FinishGame);
            _signalBus.Subscribe<PauseGameSignal>(PauseGame);
            _signalBus.Subscribe<ResumeGameSignal>(ResumeGame);

            foreach (IGameListener listener in _gameListeners)
            {
                switch (listener)
                {
                    case IGameStartListener gameStartListener:
                        _gameStartListeners.Add(gameStartListener);
                        break;
                    case IGameFinishListener gameFinishListener:
                        _gameFinishListeners.Add(gameFinishListener);
                        break;
                    case IGamePauseListener gamePauseListener:
                        _gamePauseListeners.Add(gamePauseListener);
                        break;
                    case IGameResumeListener gameResumeListener:
                        _gameResumeListeners.Add(gameResumeListener);
                        break;
                }
            }
        }

        void IDisposable.Dispose()
        {
            _signalBus.Unsubscribe<StartGameSignal>(StartGame);
            _signalBus.Unsubscribe<FinishGameSignal>(FinishGame);
            _signalBus.Unsubscribe<PauseGameSignal>(PauseGame);
            _signalBus.Unsubscribe<ResumeGameSignal>(ResumeGame);

            foreach (IDisposable disposable in _gameDisposables)
                disposable.Dispose();
        }

        private void StartGame()
        {
            foreach (IGameStartListener startListener in _gameStartListeners)
                startListener.OnGameStart();
        }

        private void PauseGame()
        {
            foreach (IGamePauseListener pauseListener in _gamePauseListeners)
                pauseListener.OnGamePause();
        }

        private void ResumeGame()
        {
            foreach (IGameResumeListener resumeListener in _gameResumeListeners)
                resumeListener.OnGameResume();
        }

        private void FinishGame()
        {
            foreach (IGameFinishListener finishListener in _gameFinishListeners)
                finishListener.OnGameFinish();
        }
    }
}