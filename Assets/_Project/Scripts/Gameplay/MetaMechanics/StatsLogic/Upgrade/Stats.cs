using _Project.Data.Config.Stats;
using DebugToolsPlus;

namespace _Project.Meta.StatsLogic.Upgrade
{
    public abstract class Stats :
        IShowStatsInfo
    {
        public int MaxLevel { get; set; }
        public int CurrentLevel { get; set; }
        public int Price { get; set; }
        public float CurrentValue { get; set; }
        public StatsView StatsView { get; set; }
        public PriceTables PriceTables { get; set; }
        public ValueTables ValueTables { get; set; }

        public bool IsMaxLevel =>
            CurrentLevel >= MaxLevel;

        public virtual void UpgradeStats()
        {
            if (IsMaxLevel)
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

        public virtual string ShowInfo()
        {
            return $"Type: {GetType().Name}\n " +
                   $"Max Level: {MaxLevel} " +
                   $"CurrentLevel: {CurrentLevel} " +
                   $"CurrentValue: {CurrentValue} " +
                   $"Price: {Price} ";
        }
    }
}