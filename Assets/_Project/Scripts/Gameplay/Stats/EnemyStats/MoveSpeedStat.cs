namespace _Project.Scripts.Gameplay.Stats.EnemyStats
{
    public struct MoveSpeedStat
    {
        public MoveSpeedStat(float speed)
        {
            Speed = speed;
        }

        public float Speed { get; private set; }
    }
}