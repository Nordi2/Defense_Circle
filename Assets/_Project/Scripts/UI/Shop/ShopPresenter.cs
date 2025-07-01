using System;
using System.Collections.Generic;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using _Project.UI.Shop;
using Zenject;

namespace _Project.Scripts.UI.Shop
{
    public class ShopPresenter :
        IDisposable,
        IInitializable
    {
        private readonly MenuPresenter _menuPresenter;
        private readonly StatsStorage _statsStorage;
        private readonly Wallet _wallet;
        private readonly ShopView _view;
        private readonly List<UpgradeCartPresenter> _upgradeCartPresenters;

        public ShopPresenter(
            Wallet wallet,
            ShopView view,
            StatsStorage statsStorage,
            MenuPresenter menuPresenter)
        {
            _wallet = wallet;
            _view = view;
            _statsStorage = statsStorage;
            _menuPresenter = menuPresenter;
            
            _upgradeCartPresenters = new List<UpgradeCartPresenter>(_statsStorage.StatsList.Count);
        }

        void IInitializable.Initialize() => 
            _menuPresenter.OnOpenShop += OpenShop;

        void IDisposable.Dispose()
        {
            _menuPresenter.OnOpenShop -= OpenShop;
            
            foreach (UpgradeCartPresenter cartPresenter in _upgradeCartPresenters)
            {
                cartPresenter.OnUpgrade -= HideShop;
                cartPresenter.Unsubscribe();
            }
        }

        public void OpenShop()
        {
            for (int i = 0; i < _upgradeCartPresenters.Count; i++) 
                _upgradeCartPresenters[i].UpdateViewCart(_statsStorage.StatsList[i]);
            
            _view.UpdateAmountMoney($"Money: {_wallet.CurrentMoney.CurrentValue}$");
            _view.OpenShop();
        }

        public void HideShop() => 
            _view.CloseShop();

        public void CreateUpgradeCarts()
        {
            for (int i = 0; i < _statsStorage.StatsList.Count; i++)
            {
                UpgradeCartView cartView = _view.SpawnCart();
                UpgradeCartPresenter cartPresenter = new UpgradeCartPresenter(cartView, _wallet);
                
                cartPresenter.OnUpgrade += HideShop;
                cartPresenter.Subscribe();
                
                _upgradeCartPresenters.Add(cartPresenter);
            }
        }
    }
}