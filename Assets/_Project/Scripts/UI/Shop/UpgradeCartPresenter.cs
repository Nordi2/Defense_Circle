using System;
using _Project.Cor.Spawner;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic.Upgrade;
using _Project.Scripts.Gameplay.Component;
using DebugToolsPlus;

namespace _Project.Scripts.UI.Shop
{
    public class UpgradeCartPresenter
    {
        public event Action OnUpgrade;
        
        private Stats _stats;
        
        private readonly UpgradeCartView _view;
        private readonly Wallet _wallet;
        private readonly WavePresenter _wavePresenter;
        
        public UpgradeCartPresenter(
            UpgradeCartView view,
            Wallet wallet,
            WavePresenter wavePresenter)
        {
            _view = view;
            _wallet = wallet;
            _wavePresenter = wavePresenter;
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
            _wavePresenter.StartWave();
            
            if (!_wallet.IsHaveMoney(_stats.Price))
            {
                D.Log("Shop", $"Have no money. Have to: {_stats.Price}, has a: {_wallet.CurrentMoney}", DColor.YELLOW,
                    true);
                return;
            }

            _wallet.SpendMoney(_stats.Price);
            _stats.UpgradeStats();
            
            OnUpgrade?.Invoke();
        }
    }
}