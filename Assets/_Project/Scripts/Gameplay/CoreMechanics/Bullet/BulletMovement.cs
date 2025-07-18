﻿using _Project.Meta.StatsLogic.NoneUpgrade;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Cor.BulletLogic
{
    [UsedImplicitly]
    public class BulletMovement : 
        ITickable
    {
        private Vector3 _direction;
        
        private readonly Transform _bulletTransform;
        private readonly float _moveSpeed;

        public BulletMovement(
            Transform bulletTransform,
            MoveSpeedStat moveSpeedStat)
        {
            _bulletTransform = bulletTransform;
            _moveSpeed = moveSpeedStat.Speed;
        }

        public void Initialize(Vector3 direction)
        {
            _direction = direction;
        }

        void ITickable.Tick()
        {
            _bulletTransform.position += _direction * (_moveSpeed * Time.deltaTime);
        }
    }
}