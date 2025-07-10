using System;
using R3;
using Zenject;

namespace _Project.Meta.Money
{
    public class WalletPresenter : 
        IInitializable ,
        IDisposable
    {
        private readonly Wallet _wallet;
        private readonly WalletView _view;
        private readonly CompositeDisposable _disposable;
        
        public WalletPresenter(WalletView view, Wallet wallet, CompositeDisposable disposable)
        {
            _view = view;
            _wallet = wallet;
            _disposable = disposable;
        }

        void IInitializable.Initialize()
        {
            _wallet
                .CurrentMoney
                .Prepend(_wallet.CurrentMoney.CurrentValue)
                .Pairwise()
                .Subscribe(pair => UpdateCurrentMoneyText(pair.Previous, pair.Current))
                .AddTo(_disposable);    
        }

        void IDisposable.Dispose() => 
            _disposable.Dispose();

        private void UpdateCurrentMoneyText(int oldValue, int newValue) => 
            _view.UpdateCurrentMoneyAmount(oldValue,newValue);
    }
}