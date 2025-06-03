using _Project.Scripts.Gameplay.BulletLogic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private SpriteRenderer _spriteSwitchColor;
        
        [SerializeField] private Color _takeDamageColor;
        [SerializeField] private Color _normalColor;
        
        public SpriteRenderer SpriteSwitchColor => _spriteSwitchColor;
        public Color NormalColor => _normalColor;
        public Color TakeDamageColor => _takeDamageColor;
        public Transform ShootPoint => _shootPoint;
        public Bullet BulletPrefab => _bulletPrefab;
    }
}