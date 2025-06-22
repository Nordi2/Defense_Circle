using _Project.Data.Config.Stats;

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