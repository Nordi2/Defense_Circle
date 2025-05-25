using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class RotationComponent : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        
        private void Update()
        {
            transform.Rotate(transform.forward, _rotationSpeed * Time.deltaTime);
        }
    }
}