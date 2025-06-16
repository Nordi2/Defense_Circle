using _Project.Scripts.Gameplay.Stats;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Project.Scripts.Data
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