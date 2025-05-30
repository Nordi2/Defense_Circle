using _Project.Scripts.Gameplay.BulletLogic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Bullet _bulletPrefab;

        public Transform ShootPoint => _shootPoint;
        public Bullet BulletPrefab => _bulletPrefab;
    }
}