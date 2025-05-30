using _Project.Scripts.UI;
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