
namespace _Project.Meta.StatsLogic.NoneUpgrade
{
    public readonly struct CollisionDamageStat 
    {
        public CollisionDamageStat(int damage)
        {
            Damage = damage;
        }

        public int Damage { get; }
    }
}