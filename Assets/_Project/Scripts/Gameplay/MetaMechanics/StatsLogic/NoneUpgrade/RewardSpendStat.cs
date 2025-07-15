namespace _Project.Meta.StatsLogic.NoneUpgrade
{
    public readonly struct RewardSpendStat
    {
        public RewardSpendStat(
            int rewardMoney,
            int spendMoney)
        {
            RewardMoney = rewardMoney;
            SpendMoney = spendMoney;
        }

        public int SpendMoney { get; }
        public int RewardMoney { get; }
    }
}