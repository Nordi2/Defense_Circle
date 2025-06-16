using System;
using DebugToolsPlus;
using JetBrains.Annotations;
using R3;

namespace Meta.Money
{
    [UsedImplicitly]
    public class Wallet
    {
        private readonly ReactiveProperty<int> _currentMoney = new(0);

        public ReadOnlyReactiveProperty<int> CurrentMoney => _currentMoney;

        public void AddMoney(int amountAddedMoney)
        {
            D.Log($"{GetType().Name}. Operation: AddMoney",
                D.FormatText($"\nOldMoney: {_currentMoney}, NewMoney: {_currentMoney.CurrentValue + amountAddedMoney}",
                    DColor.RED),
                DColor.YELLOW);

            _currentMoney.Value += amountAddedMoney;
        }

        public void SpendMoney(int amountSpentMoney)
        {
            D.Log($"{GetType().Name}. Operation: SpendMoney",
                D.FormatText(
                    $"\nOldMoney: {_currentMoney}, NewMoney: {Math.Clamp(_currentMoney.CurrentValue - amountSpentMoney, 0, int.MaxValue)}",
                    DColor.RED), DColor.YELLOW);

            if (_currentMoney.CurrentValue - amountSpentMoney <= 0)
            {
                _currentMoney.Value = 0;
                return;
            }

            _currentMoney.Value -= amountSpentMoney;
        }
    }
}