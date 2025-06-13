using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.EnemyLogic;
using _Project.Scripts.Gameplay.Money;
using _Project.Scripts.Infrastructure.Services.Factory;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
using _Project.Scripts.Infrastructure.Services.Pools;
using _Project.Scripts.Infrastructure.Signals;
using _Project.Scripts.Test;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private EnemyFacade _enemyFacadePrefab;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<StartGameSignal>();
            Container.DeclareSignal<FinishGameSignal>();
            Container.DeclareSignal<PauseGameSignal>();
            Container.DeclareSignal<ResumeGameSignal>();

            Container
                .BindInterfacesTo<GameplayBootstrapper>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<GameLoopService>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<InputService>()
                .AsSingle();

            Container
                .BindInterfacesTo<GameFactory>()
                .AsSingle();
            
            Container
                .Bind<Wallet>()
                .AsSingle();

            Container
                .BindInterfacesTo<TargetPoint>()
                .FromComponentInNewPrefabResource(AssetPath.SpawnPointPath)
                .AsSingle();

            Container
                .Bind<ShopUpgrade>()
                .FromComponentInNewPrefabResource(AssetPath.ShopUpgradePath)
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<InitialTextLoadAfterLoading>()
                .FromComponentInNewPrefabResource(AssetPath.InitialTextLoad)
                .AsSingle();

            Container
                .Bind<SpawnerTest>()
                .AsSingle()
                .WithArguments(_enemyFacadePrefab);
            
            Container
                .Bind<Camera>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.BindMemoryPool<EnemyFacade, EnemyPool>()
                .WithInitialSize(5)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefabResource(AssetPath.EnemyDefaultPath)
                .UnderTransformGroup("EnemyPool");
        }
    }
}