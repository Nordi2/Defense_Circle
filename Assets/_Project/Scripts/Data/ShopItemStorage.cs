using _Project.Meta.Stats;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Project.Data.Config
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