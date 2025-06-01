using System.Text;
using _Project.Scripts.Gameplay.Stats;
using JetBrains.Annotations;

namespace _Project.Scripts.Gameplay.EnemyLogic
{
    [UsedImplicitly]
    public class ShowStats
    {
        private readonly IStat[] _stats;

        public ShowStats(IStat[] stats)
        {
            _stats = stats;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (IStat stat in _stats) 
                stringBuilder.Append(stat.ShowInfo());
            
            return stringBuilder.ToString();
        }
    }
}