using _Project.Scripts.Gameplay.TowerLogic;
using Zenject;

namespace _Project.Scripts.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<Tower>()
                .FromComponentInNewPrefabResource(AssetPath.TowerPath)
                .AsSingle()
                .NonLazy();
            
#if UNITY_EDITOR
            Container.InstantiatePrefabResource(AssetPath.CheatManager);
#endif
        }
    }
}