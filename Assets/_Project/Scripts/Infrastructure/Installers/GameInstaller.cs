using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.TowerLogic;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<GameplayBootstrapper>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<Tower>()
                .FromComponentInNewPrefabResource(AssetPath.TowerPath)
                .AsSingle();

            Container
                .Bind<InitialTextLoadAfterLoading>()
                .FromComponentInNewPrefabResource(AssetPath.InitialTextLoad)
                .AsSingle();

            Container
                .BindInterfacesTo<InputService>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<GameLoopService>()
                .AsSingle()
                .NonLazy();
            
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