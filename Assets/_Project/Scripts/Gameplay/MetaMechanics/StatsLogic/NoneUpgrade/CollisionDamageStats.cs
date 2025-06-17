
namespace _Project.Meta.Stats.NoneUpgrade
{
    public readonly struct CollisionDamageStats :
        IStats
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