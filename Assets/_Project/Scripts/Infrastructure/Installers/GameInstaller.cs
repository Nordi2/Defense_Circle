using _Project.Scripts.Gameplay.EnemyLogic;
using _Project.Scripts.Gameplay.TowerLogic;
using UnityEngine;
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

            Container.InstantiatePrefabResource("GameEntities/Enemy",new Vector3(10,10,0),Quaternion.identity, null);
            
#if UNITY_EDITOR
            Container.InstantiatePrefabResource(AssetPath.CheatManager);
#endif
        }
    }
}