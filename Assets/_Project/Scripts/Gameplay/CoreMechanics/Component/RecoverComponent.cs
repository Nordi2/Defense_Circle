using _Project.Meta.StatsLogic.Upgrade;

namespace _Project.Cor.Component
{
    public class RecoverComponent
    {
        private readonly HealthStat _healthStat;

        public RecoverComponent(HealthStat healthStat)
        {
            _healthStat = healthStat;
        }

        public void Recover(int amountRecovered) => 
            _healthStat.AddHealth(amountRecovered);
    }
}