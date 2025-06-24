namespace _Project.Meta.StatsLogic.NoneUpgrade
{
    public readonly struct RewardSpendStats :
        IShowStatsInfo
    {
        public RewardSpendStats(
            int rewardMoney,
            int spendMoney)
        {
            RewardMoney = rewardMoney;
            SpendMoney = spendMoney;
        }

        public int SpendMoney { get; }
        public int RewardMoney { get; }

        public string ShowInfo()
        {
            return $"Reward: {RewardMoney}. " +
                   $"Spend: {SpendMoney}. ";
        }
    }
}