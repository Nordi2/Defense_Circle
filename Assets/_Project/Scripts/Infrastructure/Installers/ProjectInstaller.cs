using _Project.Infrastructure.EntryPoint;
using _Project.Infrastructure.Services.AssetManagement;
using _Project.Scripts.UI;
using Infrastructure.Services;
using Infrastructure.Services.Services.LoadData;
using Infrastructure.Services.Services.ScreenResolution;
using R3;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installer
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<EntryPoint>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<ScreenResolutionService>()
                .AsSingle();
            
            Container
                .Bind<SceneLoader>()
                .AsSingle();
            
            Container
                .Bind<CompositeDisposable>()
                .AsSingle();

            Container
                .BindInterfacesTo<LoadDataService>()
                .AsSingle();
            
            Container
                .Bind<UIRoot>()
                .FromComponentInNewPrefabResource(AssetPath.UIRootPath)
                .AsSingle();

            Container
                .Bind<Curtain>()
                .FromComponentInNewPrefabResource(AssetPath.CurtainPath)
                .AsSingle();

            Container
                .Bind<Camera>()
                .FromComponentInNewPrefabResource(AssetPath.GameplayCameraPath)
                .AsSingle();
        }
    }
}