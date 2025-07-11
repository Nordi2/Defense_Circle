using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Data.Config
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Configs/Bullet")]
    public class BulletConfig : ScriptableObject
    {
        [field: SerializeField, Min(0.5f)] public float MoveSpeed { get; private set; }
        
        [SerializeField] private Vector2Int _minMaxDamage;

        private void OnValidate()
        {
            if (_minMaxDamage.x >= _minMaxDamage.y)
            {
                _minMaxDamage.y = _minMaxDamage.x + 1;
            }
        }

        public int GetRandomDamage()
        {
            return Random.Range(_minMaxDamage.x, _minMaxDamage.y);
        }
    }
}