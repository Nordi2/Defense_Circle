
namespace _Project.Meta.StatsLogic.NoneUpgrade
{
    public readonly struct CollisionDamageStats :
        IShowStatsInfo
    {
        public CollisionDamageStats(int damage)
        {
            Damage = damage;
        }

        public int Damage { get; }

        public string ShowInfo() => 
            $"CollisionDamage: {Damage}. ";
    }
}