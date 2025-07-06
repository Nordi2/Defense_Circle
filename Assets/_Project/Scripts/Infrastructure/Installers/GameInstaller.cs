using _Project.Cor.Enemy.Mono;
using _Project.Cor.Spawner;
using _Project.Data.Config;
using _Project.Infrastructure.EntryPoint;
using _Project.Infrastructure.Services;
using _Project.Infrastructure.Services.AssetManagement;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using Infrastructure.Services;
using Infrastructure.Signals;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig _gameConfig;

        public override void InstallBindings()
        {
            BindSignals();
            
            Container
                .Bind<StatsStorage>()
                .AsSingle();

            Container
                .BindInterfacesTo<GameplayEntryPoint>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<GameLoopService>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<AssetProvider>()
                .AsSingle();

            Container
                .BindInterfacesTo<InputService>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<Wallet>()
                .AsSingle()
                .WithArguments(_gameConfig.TowerConfig.InitialMoney);

            Container
                .BindInterfacesTo<TargetPoint>()
                .FromComponentInNewPrefabResource(AssetPath.SpawnPointPath)
                .AsSingle();

            Container.BindMemoryPool<EnemyFacade, EnemyPool>()
                .WithInitialSize(5)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefabResource(AssetPath.EnemyDefaultPath)
                .UnderTransformGroup("EnemyPool");

#if UNITY_EDITOR
            Container
                .BindInterfacesAndSelfTo<UIFactory>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<GameFactory>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<InitializerEditorCheatManager>()
                .AsSingle();

#else
            Container.BindInterfacesTo<GameFactory>().AsSingle();
            Container.BindInterfacesTo<InitializerEmptyCheatManager>().AsSingle();
            Container.BindInterfacesTo<UIFactory>().AsSingle();
#endif
        }

        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<StartGameSignal>();
            Container.DeclareSignal<FinishGameSignal>();
            Container.DeclareSignal<PauseGameSignal>();
            Container.DeclareSignal<ResumeGameSignal>();
        }
    }
}