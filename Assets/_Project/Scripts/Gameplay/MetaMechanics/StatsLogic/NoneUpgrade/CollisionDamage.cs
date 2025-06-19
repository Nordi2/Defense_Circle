
namespace _Project.Meta.StatsLogic.NoneUpgrade
{
    public readonly struct CollisionDamage :
        IShowStatsInfo
    {
        public CollisionDamage(int damage)
        {
            Damage = damage;
        }

        public int Damage { get; }

        public string ShowInfo() => 
            $"CollisionDamage: {Damage}. ";
    }
}