using _Project.Data.Config.Stats;
using _Project.Extensions;
using JetBrains.Annotations;

namespace _Project.Meta.StatsLogic
{
    [UsedImplicitly]
    public class StatsBuilder
    {
        private int _currentLevel;
        private int _maxLevel;
        private int _price;
        private float _valueStats;
        private StatsView _view;
        private PriceTables _priceTables;
        private ValueTables _valueTables;
        
        public StatsBuilder WithValueTables(ValueTables valueTables)
        {
            _valueTables = valueTables;
            return this;
        }

        public StatsBuilder WithPriceTables(PriceTables priceTables)
        {
            _priceTables = priceTables;
            return this;
        }

        public StatsBuilder WithViewStats(StatsView view)
        {
            _view = view;
            return this;
        }

        public StatsBuilder WithValueStats(float value)
        {
            _valueStats = value;
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

        public StatsBuilder WithCurrentLevel(int initialLevel)
        {
            _currentLevel = initialLevel;
            return this;
        }

        public T Build<T>() where T : Upgrade.Stats, new()
        {
            T createdStats = new T();

            createdStats
                .SetMaxLevel(_maxLevel)
                .SetCurrentLevel(_currentLevel)
                .SetPrice(_price)
                .SetValueStats(_valueStats)
                .SetStatsView(_view)
                .SetPriceTables(_priceTables)
                .SetValueTables(_valueTables);

            return createdStats;
        }

        public StatsBuilder Reset()
        {
            _currentLevel = 0;
            _maxLevel = 0;
            _price = 0;
            _view = default;
            _valueTables = null;
            _priceTables = null;

            return this;
        }
    }
}