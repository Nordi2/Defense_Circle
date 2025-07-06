using System;
using Infrastructure.Signals;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services
{
    [UsedImplicitly]
    public class InputService :
        IInitializable,
        IDisposable
    {
        private readonly CompositeDisposable _disposable;
        private readonly SignalBus _signalBus;

        public InputService(CompositeDisposable disposable, SignalBus signalBus)
        {
            _disposable = disposable;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Space))
                .Take(1)
                .Subscribe(_ => GameStart())
                .AddTo(_disposable);
        }

        void IDisposable.Dispose() => 
            _disposable.Dispose();

        private void GameStart() => 
            _signalBus.Fire(new StartGameSignal());
    }
}