using UnityEngine;

namespace _Project.Scripts.Gameplay.EnemyLogic
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Transform _rotationTransform;
        [SerializeField] private DamageText _damageTextPrefab;
        [SerializeField] private ParticleSystem _dieEffect;
        
        public ParticleSystem DieEffect => _dieEffect;
        public DamageText DamageTextPrefab => _damageTextPrefab;
        public Transform RotationTransform => _rotationTransform;
    }
}