using System;
using _Project.Scripts.Infrastructure.Services.Data;
using _Project.Scripts.UI;
using R3;
using Zenject;

namespace _Project.Scripts.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<Bootstrap>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<SceneLoader>()
                .AsSingle();
            
            Container
                .Bind<CompositeDisposable>()
                .AsSingle();

            Container
                .BindInterfacesTo<DataService>()
                .AsSingle()
                .OnInstantiated<DataService>(LoadingConfig);
            
            Container
                .Bind<UIRoot>()
                .FromComponentInNewPrefabResource(AssetPath.UIRootPath)
                .AsSingle();

            Container
                .Bind<Curtain>()
                .FromComponentInNewPrefabResource(AssetPath.CurtainPath)
                .AsSingle();
        }

        private void LoadingConfig(InjectContext arg1, DataService dataService) => 
            dataService.LoadData();
    }
}