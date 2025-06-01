namespace _Project.Scripts.Gameplay.Stats.EnemyStats
{
    public struct CollisionDamageStat
    {
        public CollisionDamageStat(int damage)
        {
            Damage = damage;
        }

        public int Damage { get; private set; }
    }
}