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
        private int _valueStats;
        private StatsView _view;

        public StatsBuilder WithViewStats(StatsView view)
        {
            _view = view;
            return this;
        }

        public StatsBuilder WithValueStats(int value)
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
                .SetStatsView(_view);

            return createdStats;
        }

        public StatsBuilder Reset()
        {
            _currentLevel = 0;
            _maxLevel = 0;
            _price = 0;
            _view = default;

            return this;
        }
    }
}