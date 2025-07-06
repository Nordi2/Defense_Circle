using System;
using _Project.Cor.Spawner;
using R3;
using Zenject;

namespace _Project.Scripts.UI.Shop
{
    public class ShopController : 
        IInitializable,
        IDisposable
    {
        private readonly IEndWaveEvent _endWaveEvent;
        private readonly ShopPresenter _shopPresenter;
        private readonly CompositeDisposable _disposable;

        public ShopController(
            ShopPresenter shopPresenter,
            IEndWaveEvent endWaveEvent,
            CompositeDisposable disposable)
        {
            _shopPresenter = shopPresenter;
            _endWaveEvent = endWaveEvent;
            _disposable = disposable;
        }

        void IInitializable.Initialize()
        {
            _endWaveEvent
                .OnEndWave
                .Subscribe(OpenShop)
                .AddTo(_disposable);
        }

        void IDisposable.Dispose() => 
            _disposable.Dispose();

        private void OpenShop(Unit unit) => 
            _shopPresenter.OpenShop();
    }
}