using ModestTree;
using UnityEngine;

namespace _Project.Data.Config
{
    [CreateAssetMenu(
        fileName = "ObjectPoolConfig",
        menuName = "Configs/ObjectPoolConfig")]
    public class ObjectPoolConfig : ScriptableObject
    {
        [field: Header("Pool Settings(Enemy Default)")]
        [field: SerializeField, Min(2)] public int PoolSizeEnemyDefault { get; private set; }
        [field: SerializeField] public string NamePoolEnemyDefault {get; private set; }

        [field: Header("Pool Settings(Enemy Fast)")]
        [field: SerializeField, Min(2)] public int PoolSizeEnemyFast { get; private set; }
        [field: SerializeField] public string NamePoolEnemyFast { get; private set; }

        [field: Header("Pool Settings(Enemy Slow)")]
        [field: SerializeField, Min(2)] public int PoolSizeEnemySlow { get; private set; }
        [field: SerializeField] public string NamePoolEnemySlow { get; private set; }

        private void OnValidate()
        {
            CheckName(NamePoolEnemyDefault);
            CheckName(NamePoolEnemyFast);
            CheckName(NamePoolEnemySlow);
        }

        private void CheckName(string namePool)
        {
            if (namePool.IsEmpty())
                namePool = "None";
        }
    }
}