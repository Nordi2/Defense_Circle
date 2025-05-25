using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class TowerShoot : MonoBehaviour
    {
        [SerializeField] private float _timeReload;
        [SerializeField] private BulletMove _bulletPrefab;
        [SerializeField] private Transform _shootPoint;

        private List<Transform> _targets = new();

        private float _timer;

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeReload && _targets.Count > 0)
            {
                _timer = 0;

                Vector3 direction = _targets[0].position - _shootPoint.position;
                
                
                BulletMove bullet = Instantiate(_bulletPrefab, _shootPoint.position, transform.rotation);
                bullet.Move();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _targets.Add(other.transform);
        }
    }
}