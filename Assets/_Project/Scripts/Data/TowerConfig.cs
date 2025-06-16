using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(
        fileName = "TowerConfig",
        menuName = "Configs/TowerConfig")]
    public class TowerConfig : ScriptableObject
    {
        [field: SerializeField] public StatsConfig [] TowerStats { get; private set; }
    }
}