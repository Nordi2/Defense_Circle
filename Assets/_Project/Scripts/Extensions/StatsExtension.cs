using Data.Tables.Parent;
using Meta.Stats;
using UnityEngine;

namespace Extensions
{
    public static class StatsExtension 
    {
        public static Stats SetValueStats(this Stats stats, float value)
        {
            stats.ValueStats = value;
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

        public static Stats SetIcon(this Stats stats, Sprite icon)
        {
            stats.Icon = icon;
            return stats;
        }

        public static Stats SetPriceTable(this Stats stats, PriceTable priceTable)
        {
            stats.PriceTable = priceTable;
            return stats;
        }

        public static Stats SetStatsTables(this Stats stats, StatsTables statsTables)
        {
            stats.StatsTables = statsTables;
            return stats;
        }
    }
}
