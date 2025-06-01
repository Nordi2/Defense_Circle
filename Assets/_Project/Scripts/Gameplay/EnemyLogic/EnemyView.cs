using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.EnemyLogic
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Transform _rotationTransform;
        [SerializeField] private TextMeshPro _damageText;
        [SerializeField] private DamageText _damageTextPrefab;

        public DamageText DamageTextPrefab => _damageTextPrefab;
        public TextMeshPro DamageText => _damageText;
        public Transform RotationTransform => _rotationTransform;
    }
}