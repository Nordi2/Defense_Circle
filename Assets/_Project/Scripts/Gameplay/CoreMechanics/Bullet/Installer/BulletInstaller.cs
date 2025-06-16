using Cor.Component;
using Data.Config;
using Meta.Stats.NoneUpgrade;
using UnityEngine;
using Zenject;

namespace Cor.BulletLogic.Installer
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
                .BindInterfacesAndSelfTo<MoveSpeedStats>()
                .AsSingle()
                .WithArguments(_config.MoveSpeed);
            
            Container
                .BindInterfacesAndSelfTo<CollisionDamageStats>()
                .AsSingle()
                .WithArguments(_config.GetRandomDamage());

            Container
                .Bind<GiveDamageComponent>()
                .AsSingle();

            Container.Bind<ShowStats>().AsSingle();
        }
    }
}