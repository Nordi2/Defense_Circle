﻿using _Project.Cor.Component;
using _Project.Data.Config;
using _Project.Meta.StatsLogic.NoneUpgrade;
using UnityEngine;
using Zenject;

namespace _Project.Cor.BulletLogic.Installer
{
    public class BulletInstaller : MonoInstaller
    {
        [SerializeField] private Transform _bulletTransform;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private BulletConfig _config;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<BulletMovement>()
                .AsSingle()
                .WithArguments(_bulletTransform);

            Container
                .BindInterfacesAndSelfTo<MoveSpeedStat>()
                .AsSingle()
                .WithArguments(_config.MoveSpeed);
            
            Container
                .BindInterfacesAndSelfTo<CollisionDamageStat>()
                .AsSingle()
                .WithArguments(_config.GetRandomDamage());

            Container
                .Bind<GiveDamageComponent>()
                .AsSingle();
        }
    }
}