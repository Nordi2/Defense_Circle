using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(
        fileName = "EnemyConfig",
        menuName = "Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField, Min(10)] private Vector2Int _minMaxHealth;
        [SerializeField, Min(1)] private Vector2Int _minMaxCollisionDamage;
        [SerializeField, Min(0.5f)] private Vector2 _minMaxMoveSpeed;
        [SerializeField] private Vector2 _minMaxRotatitonSpeed;

        private void OnValidate()
        {
            if (_minMaxHealth.x > _minMaxHealth.y)
            {
                _minMaxHealth.y = _minMaxHealth.x + 1;
            }

            if (_minMaxCollisionDamage.x > _minMaxCollisionDamage.y)
            {
                _minMaxCollisionDamage.y = _minMaxCollisionDamage.x + 1;
            }

            if (_minMaxMoveSpeed.x > _minMaxMoveSpeed.y)
            {
                _minMaxMoveSpeed.y = _minMaxMoveSpeed.x + 1;
            }
        }

        public float GetRandomRotationSpeed() => 
            Random.Range(_minMaxRotatitonSpeed.x, _minMaxRotatitonSpeed.y);

        public int GetRandomValueHealth() =>
            Random.Range(_minMaxHealth.x, _minMaxHealth.y);

        public int GetRandomValueCollisionDamage() =>
            Random.Range(_minMaxCollisionDamage.x, _minMaxCollisionDamage.y);

        public float GetRandomValueMoveSpeed() =>
            Random.Range(_minMaxMoveSpeed.x, _minMaxMoveSpeed.y);
    }
}