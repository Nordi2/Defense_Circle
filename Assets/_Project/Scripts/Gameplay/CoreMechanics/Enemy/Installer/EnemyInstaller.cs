using Cor.Component;
using Cor.Enemy.Mono;
using Data.Config;
using Meta.Stats.NoneUpgrade;
using Meta.Stats.Upgrade;
using UnityEngine;
using Zenject;

namespace Cor.Enemy.Installer
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyFacade enemyFacade;
        [SerializeField] private EnemyConfig _config;
        [SerializeField] private EnemyView _view;

        public override void InstallBindings()
        {
            Container
                .Bind<EnemyView>()
                .FromInstance(_view)
                .AsSingle();

            Container
                .BindInstance(_config)
                .AsSingle();

            Container
                .Bind<AnimationEnemy>()
                .AsSingle();

            Container
                .Bind<ShowStats>()
                .AsSingle();

            Container
                .BindInterfacesTo<RotationComponent>()
                .AsSingle()
                .WithArguments(_config.GetRandomRotationSpeed());

            Container
                .BindInterfacesTo<EnemyMovement>()
                .AsSingle()
                .WithArguments(enemyFacade.transform);

            Container
                .Bind<GiveDamageComponent>()
                .AsSingle();

            Container
                .Bind<TakeDamageComponent>()
                .AsSingle();

            Container
                .Bind<EnemyCallbacks>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<HealthStats>()
                .AsSingle()
                .WithArguments(_config.GetRandomValueHealth());

            Container
                .BindInterfacesAndSelfTo<MoveSpeedStats>()
                .AsSingle()
                .WithArguments(_config.GetRandomValueMoveSpeed());

            Container
                .BindInterfacesAndSelfTo<RewardStats>()
                .AsSingle()
                .WithArguments(_config.GetRandomMoneyReward(),_config.GetRandomMoneySpend());
            
            Container
                .BindInterfacesAndSelfTo<CollisionDamageStats>()
                .AsSingle()
                .WithArguments(_config.GetRandomValueCollisionDamage());
        }
    }
}