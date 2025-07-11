using _Project.Data.Config.Stats;
using _Project.Meta.StatsLogic.Upgrade;

namespace _Project.Extensions
{
    public static class StatsExtension
    {
        public static Stats SetStatsView(this Stats stats, StatsView view)
        {
            stats.StatsView = view;
            return stats;
        }

        public static Stats SetValueStats(this Stats stats, float value)
        {
            stats.CurrentValue = value;
            return stats;
        }

        public static Stats SetCurrentLevel(this Stats stats, int initialLevel)
        {
            stats.CurrentLevel = initialLevel;
            return stats;
        }

        public static Stats SetMaxLevel(this Stats stats, int maxLevel)
        {
            stats.MaxLevel = maxLevel;
            return stats;
        }

        public static Stats SetPrice(this Stats stats, int price)
        {
            stats.Price = price;
            return stats;
        }

        public static Stats SetPriceTables(this Stats stats, PriceTables priceTables)
        {
            stats.PriceTables = priceTables;
            return stats;
        }

        public static Stats SetValueTables(this Stats stats, ValueTables valueTables)
        {
            stats.ValueTables = valueTables;
            return stats;
        }
    }
}