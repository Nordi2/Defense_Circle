namespace _Project.Scripts.Gameplay.Stats.EnemyStats
{
    public struct MoveSpeedStat : 
        IStat
    {
        public MoveSpeedStat(float speed)
        {
            Speed = speed;
        }

        public float Speed { get; private set; }

        public string ShowInfo() => 
            $"MoveSpeed: {Speed:F2}. ";
    }
}