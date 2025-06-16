namespace _Project.Scripts.Gameplay.Stats.EnemyStats
{
    public readonly struct MoveSpeedStats : 
        IStats
    {
        public MoveSpeedStats(float speed)
        {
            Speed = speed;
        }

        public float Speed { get; }

        public string ShowInfo() => 
            $"MoveSpeed: {Speed:F2}. ";
    }
}