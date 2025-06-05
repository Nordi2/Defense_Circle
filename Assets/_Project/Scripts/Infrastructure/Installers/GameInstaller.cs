using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Money;
using _Project.Scripts.Gameplay.Tower;
using _Project.Scripts.Infrastructure.Services.Data;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
using _Project.Scripts.Infrastructure.Signals;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
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
                .BindInterfacesTo<DataService>()
                .AsSingle();
            
            Container
                .Bind<Wallet>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<InitialTextLoadAfterLoading>()
                .FromComponentInNewPrefabResource(AssetPath.InitialTextLoad)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<TowerFacade>()
                .FromComponentInNewPrefabResource(AssetPath.TowerPath)
                .AsSingle()
                .OnInstantiated<TowerFacade>((_, arg2) 
                    => arg2.gameObject.SetActive(false));
            
            Container
                .Bind<Camera>()
                .FromComponentInHierarchy()
                .AsSingle();

#if UNITY_EDITOR
            Container
                .InstantiatePrefabResource(AssetPath.CheatManager);
#endif
        }
    }
}