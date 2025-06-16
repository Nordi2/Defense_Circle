using _Project.Infrastructure.EntryPoint;
using _Project.Scripts.UI;
using _Project.Static;
using Infrastructure.Services;
using R3;
using Zenject;

namespace Infrastructure.Installer
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<MainEntryPoint>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<SceneLoader>()
                .AsSingle();
            
            Container
                .Bind<CompositeDisposable>()
                .AsSingle();
            
            Container
                .Bind<UIRoot>()
                .FromComponentInNewPrefabResource(AssetPath.UIRootPath)
                .AsSingle();

            Container
                .Bind<Curtain>()
                .FromComponentInNewPrefabResource(AssetPath.CurtainPath)
                .AsSingle();
        }
    }
}