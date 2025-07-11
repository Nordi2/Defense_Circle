using _Project.Meta.StatsLogic.Upgrade;

namespace _Project.Cor.Component
{
    public class RecoverComponent
    {
        private readonly HealthStats _healthStats;

        public RecoverComponent(HealthStats healthStats)
        {
            _healthStats = healthStats;
        }

        public void Recover(int amountRecovered) => 
            _healthStats.AddHealth(amountRecovered);
    }
}