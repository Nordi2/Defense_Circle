using System;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic.Upgrade;
using _Project.Scripts.Gameplay.Component;
using DebugToolsPlus;

namespace _Project.Scripts.UI.Shop
{
    public class UpgradeCartPresenter
    {
        public event Action OnUpgrade;
        
        private Stat _stat;
        
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

        public void UpdateViewCart(Stat stat)
        {
            _stat = stat;
            _view.UpdatePrice($"Price {stat.Price}$");
            _view.UpdateCurrentLevel($"Level<br>Current: {stat.CurrentLevel}<br>Max: {stat.MaxLevel}");
            _view.UpdateNameStats(stat.StatsView.Name);
            _view.UpdateIcon(stat.StatsView.Icon);
        }

        public void Subscribe() =>
            _view.UpgradeButton.onClick.AddListener(BuyUpgrade);

        public void Unsubscribe() =>
            _view.UpgradeButton.onClick.RemoveListener(BuyUpgrade);

        private void BuyUpgrade()
        {
            _wavePresenter.StartWave();
            
            if (!_wallet.IsHaveMoney(_stat.Price))
            {
                D.Log("Shop", $"Have no money. Have to: {_stat.Price}, has a: {_wallet.CurrentMoney}", DColor.YELLOW,
                    true);
                return;
            }

            _wallet.SpendMoney(_stat.Price);
            _stat.UpgradeStats();
            
            OnUpgrade?.Invoke();
        }
    }
}