using _Project.Cor.Enemy;
using _Project.Cor.Enemy.Mono;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Project.Data.Config
{
    [CreateAssetMenu(
        fileName = "EnemyConfig",
        menuName = "Configs/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyType Type { get; private set; } = EnemyType.Default;
        [field: SerializeField] public EnemyFacade EnemyPrefab { get; private set; }

        [SerializeField, Min(10)] private Vector2Int _minMaxHealth;
        [SerializeField, Min(1)] private Vector2Int _minMaxCollisionDamage;
        [SerializeField, Min(0.1f)] private Vector2 _minMaxMoveSpeed;
        [SerializeField] private Vector2 _minMaxRotatitonSpeed;
        [SerializeField] private Vector2Int _minMaxMoneyReward;
        [SerializeField] private Vector2Int _minMaxMoneySpend;


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

        public int GetRandomMoneySpend() =>
            Random.Range(_minMaxMoneySpend.x, _minMaxMoneySpend.y);

        public int GetRandomMoneyReward() =>
            Random.Range(_minMaxMoneyReward.x, _minMaxMoneyReward.y);

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