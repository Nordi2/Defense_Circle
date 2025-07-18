﻿using UnityEngine;

namespace _Project.Cor.Enemy.Mono
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Transform _rotationTransform;
        [SerializeField] private DamageText _damageTextPrefab;
        [SerializeField] private ParticleSystem _dieEffect;
        [SerializeField] private TrailRenderer _trailRenderer;
        
        public ParticleSystem DieEffect => _dieEffect;
        public DamageText DamageTextPrefab => _damageTextPrefab;
        public Transform RotationTransform => _rotationTransform;

        private void OnDisable()
        {
            _trailRenderer.Clear();
        }
    }
}