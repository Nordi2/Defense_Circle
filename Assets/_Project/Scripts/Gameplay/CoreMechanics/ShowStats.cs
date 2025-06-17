using System.Text;
using _Project.Meta.Stats;
using JetBrains.Annotations;

namespace _Project.Cor
{
    [UsedImplicitly]
    public class ShowStats
    {
        private readonly IStats[] _stats;

        public ShowStats(IStats[] stats)
        {
            _stats = stats;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (IStats stat in _stats) 
                stringBuilder.Append(stat.ShowInfo());
            
            return stringBuilder.ToString();
        }
    }
}