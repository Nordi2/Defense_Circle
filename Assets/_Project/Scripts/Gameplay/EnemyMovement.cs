using UnityEngine;

namespace _Project.Scripts
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _moveSpeed = 5f;

        private void Update()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _target.position,
                _moveSpeed * Time.deltaTime);
        }
    }
}