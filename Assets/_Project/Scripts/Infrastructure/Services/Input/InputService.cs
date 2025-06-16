using System;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services
{
    [UsedImplicitly]
    public class InputService :
        IInputService,
        IInitializable,
        IDisposable
    {
        public Subject<Unit> OnClickSpaceButton { get; } = new();
        
        private readonly CompositeDisposable _disposable;

        public InputService(CompositeDisposable disposable)
        {
            _disposable = disposable;
        }

        void IInitializable.Initialize()
        {
            Observable.EveryUpdate()
                .Where(_ => UnityEngine.Input.GetKeyDown(KeyCode.Space))
                .Take(1)
                .Subscribe(_ => OnClickSpaceButton.OnNext(Unit.Default))
                .AddTo(_disposable);
        }

        void IDisposable.Dispose()
        {
            _disposable.Dispose();
        }
    }
}