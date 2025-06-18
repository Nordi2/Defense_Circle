using System.Collections.Generic;
using System.Text;
using _Project.Meta.StatsLogic;
using _Project.Meta.StatsLogic.Upgrade;
using JetBrains.Annotations;

namespace _Project.Cor
{
    [UsedImplicitly]
    public class ShowStatsService
    {
        private readonly List<IShowStatsInfo> _stats;
        
        public ShowStatsService(List<IShowStatsInfo> stats)
        {
            _stats = stats;
        }

        public void AddStats(Stats stats) => 
            _stats.Add(stats);

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (IShowStatsInfo stat in _stats)
                stringBuilder.Append(stat.ShowInfo());

            return stringBuilder.ToString();
        }
    }
}