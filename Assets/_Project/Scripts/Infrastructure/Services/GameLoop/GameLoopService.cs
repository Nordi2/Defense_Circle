using System;
using System.Collections.Generic;
using DebugToolsPlus;
using Infrastructure.Signals;
using JetBrains.Annotations;
using Zenject;

namespace Infrastructure.Services
{
    [UsedImplicitly]
    public class GameLoopService :
        IInitializable,
        IDisposable,
        ITickable
    {
        private readonly SignalBus _signalBus;
        private readonly IGameListener[] _gameListeners;

        private readonly List<IGameStartListener> _gameStartListeners;
        private readonly List<IGameFinishListener> _gameFinishListeners;
        private readonly List<IGamePauseListener> _gamePauseListeners;
        private readonly List<IGameResumeListener> _gameResumeListeners;

        private readonly List<ITickable> _tickables;
        private readonly List<IInitializable> _initializables;
        private readonly List<IDisposable> _disposables;

        private GameState _currentGameState = GameState.None;

        public GameLoopService(IGameListener[] gameListeners, SignalBus signalBus)
        {
            _gameListeners = gameListeners;
            _signalBus = signalBus;

            _gameStartListeners = new List<IGameStartListener>();
            _gameFinishListeners = new List<IGameFinishListener>();
            _gamePauseListeners = new List<IGamePauseListener>();
            _gameResumeListeners = new List<IGameResumeListener>();
            _tickables = new List<ITickable>();
            _initializables = new List<IInitializable>();
            _disposables = new List<IDisposable>();
        }

        public void AddGameListener(IGameListener listener)
        {
            if (listener is IGameStartListener gameStartListener)
                _gameStartListeners.Add(gameStartListener);
        }

        public void AddInitializable(params IInitializable[] initializables) =>
            _initializables.AddRange(initializables);

        public void AddDisposable(params IDisposable[] disposable) => 
            _disposables.AddRange(disposable);

        public void AddTickable(params ITickable[] tickable) => 
            _tickables.AddRange(tickable);

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

            foreach (IInitializable initializable in _initializables)
                initializable.Initialize();
        }

        void IDisposable.Dispose()
        {
            _signalBus.Unsubscribe<StartGameSignal>(StartGame);
            _signalBus.Unsubscribe<FinishGameSignal>(FinishGame);
            _signalBus.Unsubscribe<PauseGameSignal>(PauseGame);
            _signalBus.Unsubscribe<ResumeGameSignal>(ResumeGame);

            foreach (IDisposable disposable in _disposables)
                disposable.Dispose();
        }

        void ITickable.Tick()
        {
            if (_currentGameState == GameState.Pause)
                return;

            for (int i = 0; i < _tickables.Count; i++)
                _tickables[i].Tick();
        }

        private void StartGame()
        {
            if (_currentGameState != GameState.None)
                return;

            foreach (IGameStartListener startListener in _gameStartListeners)
                startListener.OnGameStart();

            _currentGameState = GameState.Loop;

            D.Log(GetType().Name, "Game started", DColor.PURPLE, true);
        }

        private void PauseGame()
        {
            if (_currentGameState != GameState.Loop)
                return;

            _currentGameState = GameState.Pause;

            foreach (IGamePauseListener pauseListener in _gamePauseListeners)
                pauseListener.OnGamePause();

            D.Log(GetType().Name, "Game pause", DColor.PURPLE, true);
        }

        private void ResumeGame()
        {
            if (_currentGameState != GameState.Pause)
                return;

            _currentGameState = GameState.Loop;

            foreach (IGameResumeListener resumeListener in _gameResumeListeners)
                resumeListener.OnGameResume();
            
            D.Log(GetType().Name, "Game resume", DColor.PURPLE, true);
        }

        private void FinishGame()
        {
            if (_currentGameState != GameState.Loop)
                return;

            foreach (IGameFinishListener finishListener in _gameFinishListeners)
                finishListener.OnGameFinish();


            D.Log(GetType().Name, "Game finish", DColor.PURPLE, true);
        }
    }
}