namespace _Project.Scripts.Gameplay.Stats.EnemyStats
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