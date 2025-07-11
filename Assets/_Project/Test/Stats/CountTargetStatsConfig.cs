/*using _Project.Data.Stats.Config.Parent;
using _Project.Data.Tables;
using UnityEngine;

namespace _Project.Data.Stats.Config
{
    [CreateAssetMenu(
        fileName = "CountTargetsStats_Config",
        menuName = "ScriptableObjects/Configs/CountTargetsStatsConfig")]
    public class CountTargetStatsConfig : StatsConfig
    {
        [field: SerializeField] public CountTargetsTables CountTargetsTables { get; private set; }

        private void OnValidate()
        {
            CountTargetsTables.OnValidate(MaxLevel);
            PriceTable.OnValidate(MaxLevel);
        }
    }
}*/