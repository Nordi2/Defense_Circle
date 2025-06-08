namespace _Project.Scripts.Gameplay.Stats.EnemyStats
{
    public readonly struct MoveSpeedStat : 
        IStat
    {
        public MoveSpeedStat(float speed)
        {
            Speed = speed;
        }

        public float Speed { get; }

        public string ShowInfo() => 
            $"MoveSpeed: {Speed:F2}. ";
    }
}