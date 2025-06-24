
namespace _Project.Meta.StatsLogic.NoneUpgrade
{
    public readonly struct MoveSpeedStats : 
        IShowStatsInfo
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