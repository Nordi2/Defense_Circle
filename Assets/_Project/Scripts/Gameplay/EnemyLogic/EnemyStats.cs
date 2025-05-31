namespace _Project.Scripts.Gameplay.EnemyLogic
{
    public struct EnemyStats
    {
        public int Health { get;private set; }
        public int CollisionDamage { get;private set; }
        public float MovementSpeed { get;private set; }
        
        public EnemyStats(
            int health,
            float movementSpeed,
            int collisionDamage)
        {
            Health = health;
            MovementSpeed = movementSpeed;
            CollisionDamage = collisionDamage;
        }

        public override string ToString()
        {
            return "Generation Stats. " +
                   $"Health: {Health}," +
                   $"Collision Damage: {CollisionDamage}," +
                   $"Movement Speed: {MovementSpeed:F2}";
        }
    }
}