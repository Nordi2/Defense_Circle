using _Project.Meta.Money;
using _Project.Meta.StatsLogic.Upgrade;
using DebugToolsPlus;

namespace _Project.Scripts.UI.Shop
{
    public class UpgradeCartPresenter
    {
        private readonly UpgradeCartView _view;
        private readonly Wallet _wallet;
        private Stats _stats;

        public UpgradeCartPresenter(
            UpgradeCartView view,
            Wallet wallet)
        {
            _view = view;
            _wallet = wallet;
        }

        public void UpdateViewCart(Stats stats)
        {
            _stats = stats;
            _view.UpdatePrice($"Price {stats.Price}$");
            _view.UpdateCurrentLevel($"Level<br>Current: {stats.CurrentLevel}<br>Max: {stats.MaxLevel}");
            _view.UpdateNameStats(stats.StatsView.Name);
            _view.UpdateIcon(stats.StatsView.Icon);
        }

        public void Subscribe() =>
            _view.UpgradeButton.onClick.AddListener(BuyUpgrade);

        public void Unsubscribe() =>
            _view.UpgradeButton.onClick.RemoveListener(BuyUpgrade);

        private void BuyUpgrade()
        {
            if (_wallet.IsHaveMoney(_stats.Price))
            {
                _wallet.SpendMoney(_stats.Price);
                return;
            }

            D.Log("Shop", $"Have no money. Have to: {_stats.Price}, has a: {_wallet.CurrentMoney}", DColor.YELLOW,
                true);
        }
    }
}