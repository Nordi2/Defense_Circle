using UnityEngine;

namespace _Project.Data.Config
{
    [CreateAssetMenu(
        fileName = "TowerConfig",
        menuName = "Configs/Tower")]
    public class TowerConfig : ScriptableObject
    {
        [field: SerializeField] public StatsConfig[] Stats {get; private set; }
        [field: SerializeField] public int InitialMoney { get; private set; }
    }
}