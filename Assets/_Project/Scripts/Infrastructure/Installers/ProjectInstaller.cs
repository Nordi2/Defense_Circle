using _Project.Scripts.UI;
using Zenject;

namespace _Project.Scripts.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<UIRoot>()
                .FromComponentInNewPrefabResource(AssetPath.UIRootPath)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<Curtain>()
                .FromComponentInNewPrefabResource(AssetPath.CurtainPath)
                .AsSingle()
                .NonLazy();
        }
    }
}
