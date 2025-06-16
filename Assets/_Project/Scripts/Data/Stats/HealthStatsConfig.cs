using Data.Stats.Config.Parent;
using Data.Tables;
using UnityEngine;

namespace Data.Stats.Config
{
    [CreateAssetMenu(
        fileName = "HealthStats_Config",
        menuName = "ScriptableObjects/Configs/Stats/HealthStats")]
    public class HealthStatsConfig : StatsConfig
    {
        [field: SerializeField] public HealthTable HealthTable { get; private set; }

        private void OnValidate()
        {
            HealthTable.OnValidate(MaxLevel);
            PriceTable.OnValidate(MaxLevel);
        }
    }
}