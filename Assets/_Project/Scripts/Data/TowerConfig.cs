using _Project.Data.Stats.Config.Parent;
using UnityEngine;

namespace _Project.Data.Config
{
    [CreateAssetMenu(
        fileName = "TowerConfig",
        menuName = "Configs/TowerConfig")]
    public class TowerConfig : ScriptableObject
    {
        [field: SerializeField] public StatsConfig [] TowerStats { get; private set; }
    }
}