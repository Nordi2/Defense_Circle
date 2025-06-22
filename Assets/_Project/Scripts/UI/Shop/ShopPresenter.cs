using System;
using System.Collections.Generic;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using _Project.Meta.StatsLogic.Upgrade;
using _Project.UI.Shop;
using Random = UnityEngine.Random;

namespace _Project.Scripts.UI.Shop
{
    public class ShopPresenter : 
        IDisposable
    {
        private readonly StatsStorage _statsStorage;
        private readonly Wallet _wallet;
        private readonly ShopView _view;
        private readonly List<UpgradeCartPresenter> _upgradeCartPresenters = new();
        private readonly HashSet<int> _uniqueIndexes = new();

        public ShopPresenter(
            Wallet wallet,
            ShopView view,
            StatsStorage statsStorage)
        {
            _wallet = wallet;
            _view = view;
            _statsStorage = statsStorage;
        }

        public void OpenShop()
        {
            GenerateIndex();

            for (int i = 0; i < _upgradeCartPresenters.Count; i++)
            {
                _upgradeCartPresenters[i].UpdateViewCart(GetCart(i));
            }

            _view.UpdateAmountMoney($"Money: {_wallet.CurrentMoney.CurrentValue}$");
            _view.OpenShop();
        }

        public void HideShop()
        {
            _uniqueIndexes.Clear();
            _view.CloseShop();
        }

        public void CreateUpgradeCarts()
        {
            for (int i = 0; i < _statsStorage.StatsList.Count; i++)
            {
                UpgradeCartView cartView = _view.SpawnCart();
                UpgradeCartPresenter cartPresenter = new UpgradeCartPresenter(cartView, _wallet);
                _upgradeCartPresenters.Add(cartPresenter);
                cartPresenter.Subscribe();
            }
        }

        private void GenerateIndex()
        {
            while (_uniqueIndexes.Count < _statsStorage.StatsList.Count)
            {
                int randomIndex = Random.Range(0, _statsStorage.StatsList.Count);
                _uniqueIndexes.Add(randomIndex);
            }
        }

        private Stats GetCart(int index)
        {
            if (_uniqueIndexes.TryGetValue(index, out int value))
                return _statsStorage.StatsList[value];

            throw new Exception();
        }

        void IDisposable.Dispose()
        {
            foreach (UpgradeCartPresenter cartPresenter in _upgradeCartPresenters)
                cartPresenter.Unsubscribe();
        }
    }
}