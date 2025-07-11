using System;
using DebugToolsPlus;
using JetBrains.Annotations;
using R3;

namespace _Project.Meta.Money
{
    [UsedImplicitly]
    public class Wallet
    {
        private readonly ReactiveProperty<int> _currentMoney;

        public ReadOnlyReactiveProperty<int> CurrentMoney => _currentMoney;

        public Wallet(int initialMoney)
        {
            _currentMoney = new ReactiveProperty<int>(initialMoney);
        }

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

            _currentMoney.Value = Math.Clamp(_currentMoney.Value - amountSpentMoney, 0, int.MaxValue);
        }

        public bool IsHaveMoney(int amountSpentMoney) => 
            _currentMoney.Value >= amountSpentMoney;
    }
}