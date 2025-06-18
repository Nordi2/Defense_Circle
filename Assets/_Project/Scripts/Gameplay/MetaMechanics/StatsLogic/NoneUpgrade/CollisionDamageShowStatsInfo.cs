
namespace _Project.Meta.StatsLogic.NoneUpgrade
{
    public readonly struct CollisionDamageShowStatsInfo :
        IShowStatsInfo
    {
        public CollisionDamageShowStatsInfo(int damage)
        {
            Damage = damage;
        }

        public int Damage { get; }

        public string ShowInfo() => 
            $"CollisionDamage: {Damage}. ";
    }
}