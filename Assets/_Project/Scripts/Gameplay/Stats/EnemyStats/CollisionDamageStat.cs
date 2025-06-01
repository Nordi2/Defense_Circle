namespace _Project.Scripts.Gameplay.Stats.EnemyStats
{
    public struct CollisionDamageStat :
        IStat
    {
        public CollisionDamageStat(int damage)
        {
            Damage = damage;
        }

        public int Damage { get; private set; }

        public string ShowInfo() => 
            $"CollisionDamage: {Damage}. ";
    }
}