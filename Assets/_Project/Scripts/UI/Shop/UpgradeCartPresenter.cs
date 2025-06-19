using _Project.Meta.Money;
using _Project.Meta.StatsLogic.Upgrade;

namespace _Project.Scripts.UI.Shop
{
    public class UpgradeCartPresenter
    {
        private readonly UpgradeCartView _view;
        private Wallet _wallet;
        private Stats _stats;
        
        public UpgradeCartPresenter(
            UpgradeCartView view,
            Wallet wallet,
            Stats stats)
        {
            _view = view;
            _wallet = wallet;
            _stats = stats;

            _view.UpdateCurrentLevel($"Level<br>Current: {stats.CurrentLevel}<br>Max: {_stats.MaxLevel}");
            _view.UpdateNameStats(_stats.StatsView.Name);
            _view.UpdateIcon(_stats.StatsView.Icon);
        }
    }
}