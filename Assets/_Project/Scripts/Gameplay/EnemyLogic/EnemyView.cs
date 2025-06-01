using UnityEngine;

namespace _Project.Scripts.Gameplay.EnemyLogic
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Transform _rotationTransform;

        public Transform RotationTransform => _rotationTransform;
    }
}