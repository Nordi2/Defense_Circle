using AYellowpaper.SerializedCollections;
using Meta.Stats;
using UnityEngine;

namespace Data.Config
{
    [CreateAssetMenu(
        fileName = "ShopItemStorage",
        menuName = "Configs/Shop/ShopStorage")]
    public class ShopItemStorage : ScriptableObject
    {
        [SerializedDictionary("Type","Config")]
        public SerializedDictionary<StatsType,NewStatsConfig> StatsConfigs;
    }
}