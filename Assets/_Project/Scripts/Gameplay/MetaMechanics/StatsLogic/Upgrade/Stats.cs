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

        private bool _isMaxLevel =>
            CurrentLevel > MaxLevel;

        public void UpgradeStats()
        {
            if (_isMaxLevel)
                return;

            CurrentLevel++;
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