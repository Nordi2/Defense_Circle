using _Project.Data.Config.Stats;
using _Project.Meta.StatsLogic.Upgrade;

namespace _Project.Extensions
{
    public static class StatsExtension
    {
        public static Stat SetStatsView(this Stat stat, StatsView view)
        {
            stat.StatsView = view;
            return stat;
        }

        public static Stat SetValueStats(this Stat stat, float value)
        {
            stat.CurrentValue = value;
            return stat;
        }

        public static Stat SetCurrentLevel(this Stat stat, int initialLevel)
        {
            stat.CurrentLevel = initialLevel;
            return stat;
        }

        public static Stat SetMaxLevel(this Stat stat, int maxLevel)
        {
            stat.MaxLevel = maxLevel;
            return stat;
        }

        public static Stat SetPrice(this Stat stat, int price)
        {
            stat.Price = price;
            return stat;
        }

        public static Stat SetPriceTables(this Stat stat, PriceTables priceTables)
        {
            stat.PriceTables = priceTables;
            return stat;
        }

        public static Stat SetValueTables(this Stat stat, ValueTables valueTables)
        {
            stat.ValueTables = valueTables;
            return stat;
        }
    }
}