using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.TestScripts
{
    public class WeaponTower : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}