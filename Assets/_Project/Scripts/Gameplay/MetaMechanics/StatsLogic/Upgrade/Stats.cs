using _Project.Data.Config.Stats;
using DebugToolsPlus;
using UnityEngine;

namespace _Project.Meta.StatsLogic.Upgrade
{
    public abstract class Stats :
        IShowStatsInfo
    {
        public int MaxLevel { get; set; }
        public int CurrentLevel { get; set; }
        public int Price { get; set; }
        public int CurrentValue { get; set; }
        public StatsView StatsView { get; set; }
        public PriceTables PriceTables { get; set; }
        public ValueTables ValueTables { get; set; }

        private bool _isMaxLevel =>
            CurrentLevel > MaxLevel;

        public void UpgradeStats()
        {
            if (_isMaxLevel)
                return;

            CurrentLevel++;

            D.Log(GetType().Name, D.FormatText("Upgrade Stats: " +
                                               $"Current Level: {CurrentLevel}," +
                                               $"Old Level: {CurrentLevel - 1}," +
                                               $"Max Level: {MaxLevel}", DColor.RED),
                DColor.YELLOW);

            CurrentValue = ValueTables.GetValue(CurrentLevel);
            Price = PriceTables.GetPrice(CurrentLevel);
        }

        public string ShowInfo()
        {
            return $"Type: {GetType().Name}\n " +
                   $"Max Level: {MaxLevel} " +
                   $"CurrentLevel: {CurrentLevel} " +
                   $"CurrentValue: {CurrentValue} " +
                   $"Price: {Price} ";
        }
    }
}