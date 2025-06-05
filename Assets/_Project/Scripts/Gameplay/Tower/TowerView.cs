using _Project.Scripts.Gameplay.BulletLogic;
using _Project.Scripts.Gameplay.Tower.Animation.AnimationSettings;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Tower
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Bullet _bulletPrefab;

        [SerializeField] private TowerInitialSpawnSettings _animationSpawnSettings;
        [SerializeField] private TowerTakeDamageSettings _animationTakeDamageSettings;
        [SerializeField] private TowerDeathSettings _animationDeathSettings;

        public TowerDeathSettings AnimationDeathSettings => _animationDeathSettings;
        public TowerTakeDamageSettings AnimationTakeDamageSettings => _animationTakeDamageSettings;
        public TowerInitialSpawnSettings AnimationSpawnSettings => _animationSpawnSettings;
        public Transform ShootPoint => _shootPoint;
        public Bullet BulletPrefab => _bulletPrefab;
    }
}