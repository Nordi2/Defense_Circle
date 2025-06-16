namespace Meta.Stats.NoneUpgrade
{
    public readonly struct RewardStats :
        IStats
    {
        public RewardStats(
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