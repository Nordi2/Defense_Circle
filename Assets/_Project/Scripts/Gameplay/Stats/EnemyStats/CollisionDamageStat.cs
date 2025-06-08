namespace _Project.Scripts.Gameplay.Stats.EnemyStats
{
    public readonly struct CollisionDamageStat :
        IStat
    {
        public CollisionDamageStat(int damage)
        {
            Damage = damage;
        }

        public int Damage { get; }

        public string ShowInfo() => 
            $"CollisionDamage: {Damage}. ";
    }
}