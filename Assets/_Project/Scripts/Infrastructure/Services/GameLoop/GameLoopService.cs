using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.Services.GameLoop
{
    public class GameLoopService :
        IInitializable
    {
        private readonly IGameListener[] _gameListeners;

        private readonly List<IGameStartListener> _gameStartListeners;
        private readonly List<IGameFinishListener> _gameFinishListeners;
        private readonly List<IGamePauseListener> _gamePauseListeners;
        private readonly List<IGameResumeListener> _gameResumeListeners;

        public GameLoopService(IGameListener[] gameListeners)
        {
            _gameListeners = gameListeners;

            _gameStartListeners = new List<IGameStartListener>();
            _gameFinishListeners = new List<IGameFinishListener>();
            _gamePauseListeners = new List<IGamePauseListener>();
            _gameResumeListeners = new List<IGameResumeListener>();
        }

        void IInitializable.Initialize()
        {
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

        public void StartGame()
        {
            foreach (IGameStartListener startListener in _gameStartListeners)
                startListener.OnGameStart();
        }

        public void PauseGame()
        {
            foreach (IGamePauseListener pauseListener in _gamePauseListeners)
                pauseListener.OnGamePause();
        }

        public void ResumeGame()
        {
            foreach (IGameResumeListener resumeListener in _gameResumeListeners)
                resumeListener.OnGameResume();
        }

        public void FinishGame()
        {
            foreach (IGameFinishListener finishListener in _gameFinishListeners)
                finishListener.OnGameFinish();
        }
    }
}