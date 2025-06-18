
namespace _Project.Meta.StatsLogic.NoneUpgrade
{
    public readonly struct MoveSpeedShowStatsInfo : 
        IShowStatsInfo
    {
        public MoveSpeedShowStatsInfo(float speed)
        {
            Speed = speed;
        }

        public float Speed { get; }

        public string ShowInfo() => 
            $"MoveSpeed: {Speed:F2}. ";
    }
}