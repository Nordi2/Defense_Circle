using UnityEngine;

namespace _Project.Scripts
{
    public class BulletMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        public void Move()
        {
            _rigidbody2D.velocity = transform.up * _moveSpeed;
        }
    }
}
