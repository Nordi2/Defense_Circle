using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.BulletLogic
{
    public class Bullet : MonoBehaviour
    {
        public Vector3 targetPosition;
        public float _moveSpeed;
        public Rigidbody2D _Rigidbody2D;
        private void Update()
        {
            transform.position += targetPosition * (_moveSpeed * Time.deltaTime);
           // _Rigidbody2D.MovePosition(_Rigidbody2D.position + targetPosition * (_moveSpeed * Time.deltaTime));
        }
    }
}