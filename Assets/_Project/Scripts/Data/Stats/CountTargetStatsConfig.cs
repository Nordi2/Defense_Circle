using System;
using _Project.Scripts.Data.Stats.Tables;
using UnityEngine;

namespace _Project.Scripts.Data.Stats
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
}