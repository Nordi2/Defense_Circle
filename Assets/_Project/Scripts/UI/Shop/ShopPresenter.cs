using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using _Project.Meta.StatsLogic.Upgrade;
using _Project.Static;
using _Project.UI.Shop;
using R3;
using Random = UnityEngine.Random;

namespace _Project.Scripts.UI.Shop
{
    public class ShopPresenter
    {
        private readonly StatsStorage _statsStorage;
        private readonly Wallet _wallet;
        private readonly ShopView _view;
        private readonly CompositeDisposable _disposable;

        public ShopPresenter(
            Wallet wallet,
            ShopView view,
            CompositeDisposable disposable,
            StatsStorage statsStorage)
        {
            _wallet = wallet;
            _view = view;
            _disposable = disposable;
            _statsStorage = statsStorage;
        }

        public void OpenShop() =>
            _view.OpenShop();

        public void HideShop() =>
            _view.CloseShop();

        public void CreateUpgradeCarts()
        {
            for (int i = 0; i < Constants.AmountUpgradeCart; i++)
            {
                UpgradeCartView cartView = _view.SpawnCart();
                UpgradeCartPresenter cartPresenter = new UpgradeCartPresenter(cartView, _wallet, RandomizeCart());
            }
        }

        private Stats RandomizeCart()
        {
            int randomIndex = Random.Range(0, _statsStorage.StatsList.Count);
            return _statsStorage.StatsList[randomIndex];
        }
    }
}