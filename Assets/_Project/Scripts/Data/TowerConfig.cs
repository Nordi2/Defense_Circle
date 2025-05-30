using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(
        fileName = "TowerConfig",
        menuName = "Configs/TowerConfig")]
    public class TowerConfig : ScriptableObject
    {
        [field: SerializeField, Min(1)] public int MaxHealth { get; private set; } = 100;
    }
}