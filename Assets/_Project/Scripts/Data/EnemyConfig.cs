using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "EnemyConfig",menuName = "Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public int MaxHealth { get; private set; } = 100;
        [field: SerializeField] public int CollisionDamage { get; private set; } = 10;
        [field: SerializeField] public float MoveSpeed { get; private set; } = 1.5f;
    }
}