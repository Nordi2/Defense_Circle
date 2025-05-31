using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(
        fileName = "TowerConfig",
        menuName = "Configs/TowerConfig")]
    public class TowerConfig : ScriptableObject
    {
        [field: SerializeField, Min(1)] public int MaxHealth { get; private set; } = 100;
        [field: SerializeField,Min(1)] public int AmountTarget { get; private set; } = 1;
        [field: SerializeField, Min(0.25f)] public float ShootRate { get; private set; } = 0.25f;
    }
}