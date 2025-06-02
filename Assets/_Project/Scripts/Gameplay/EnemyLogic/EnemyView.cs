using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.EnemyLogic
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Transform _rotationTransform;
        [SerializeField] private DamageText _damageTextPrefab;
        [SerializeField] private ParticleSystem _dieEffect;
        [SerializeField] private GameObject _moneyPrefab;

        public GameObject MoneyPrefab => _moneyPrefab;
        public ParticleSystem DieEffect => _dieEffect;
        public DamageText DamageTextPrefab => _damageTextPrefab;
        public Transform RotationTransform => _rotationTransform;
    }
}