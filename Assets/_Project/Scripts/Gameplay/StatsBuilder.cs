using _Project.Scripts.Data;
using _Project.Scripts.Extensions;
using _Project.Scripts.Gameplay.Stats;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class StatsBuilder
    {
        private int _currentLevel;
        private int _maxLevel;
        private int _price;
        private float _valueStats;
        private Sprite _icon;
        private PriceTable _priceTable;
        private StatsTables _statsTables;

        public StatsBuilder WithValueStats(float valueStats)
        {
            _valueStats = valueStats;
            return this;
        }

        public StatsBuilder WithStatsTable(StatsTables statsTable)
        {
            _statsTables = statsTable;
            return this;
        }

        public StatsBuilder WithPriceTable(PriceTable priceTable)
        {
            _priceTable = priceTable;

            return this;
        }

        public StatsBuilder WithCurrentLevel(int initialLevel)
        {
            _currentLevel = initialLevel;
            return this;
        }

        public StatsBuilder WithMaxLevel(int maxLevel)
        {
            _maxLevel = maxLevel;
            return this;
        }

        public StatsBuilder WithPrice(int price)
        {
            _price = price;
            return this;
        }

        public StatsBuilder WithIcon(Sprite icon)
        {
            _icon = icon;
            return this;
        }

        public T Build<T>() where T : StatsLogic.Stats, new()
        {
            T createdStats = new T();

            createdStats
                .SetCurrentLevel(_currentLevel)
                .SetMaxLevel(_maxLevel)
                .SetPrice(_price)
                .SetIcon(_icon)
                .SetPriceTable(_priceTable)
                .SetStatsTables(_statsTables)
                .SetValueStats(_valueStats);
            
            return createdStats;
        }

        public StatsBuilder Reset()
        {
            _currentLevel = 0;
            _maxLevel = 0;
            _price = 0;
            _icon = null;
            _priceTable = null;

            return this;
        }
    }
}