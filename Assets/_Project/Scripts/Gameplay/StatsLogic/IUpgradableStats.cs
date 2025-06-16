namespace _Project.Scripts.Gameplay.Stats
{
    public interface IUpgradableStats : IStats
    {
        int CurrentLevel { get; }
        void UpgradeStats(int newValue);
    }
}