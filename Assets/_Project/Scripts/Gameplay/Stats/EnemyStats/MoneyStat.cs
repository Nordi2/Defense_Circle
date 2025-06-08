namespace _Project.Scripts.Gameplay.Stats.EnemyStats
{
    public readonly struct MoneyStat :
        IStat
    {
        public MoneyStat(
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