using _Project.Cor.BulletLogic.Mono;
using _Project.Cor.Observers;
using _Project.Cor.Tower.Animation.AnimationSettings;
using _Project.Scripts.Gameplay.Component;
using UnityEngine;

namespace _Project.Cor.Tower.Mono
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private BulletFacade _bulletPrefab;
        [SerializeField] private HealthView _healthView;

        [SerializeField] private TowerInitialSpawnSettings _animationSpawnSettings;
        [SerializeField] private TowerTakeDamageSettings _animationTakeDamageSettings;
        [SerializeField] private TowerDeathSettings _animationDeathSettings;

        [SerializeField] private EnemyObserver _enemyObserver;

        public EnemyObserver EnemyObserver => _enemyObserver;
        public HealthView HealthView => _healthView;
        public TowerDeathSettings AnimationDeathSettings => _animationDeathSettings;
        public TowerTakeDamageSettings AnimationTakeDamageSettings => _animationTakeDamageSettings;
        public TowerInitialSpawnSettings AnimationSpawnSettings => _animationSpawnSettings;
        public Transform ShootPoint => _shootPoint;
        public BulletFacade BulletPrefab => _bulletPrefab;
    }
}