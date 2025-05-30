using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Component;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    public class TowerInstaller : MonoInstaller
    {
        [SerializeField] private TowerConfig _config;
        [SerializeField] private Tower _tower;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(_tower)
                .AsSingle()
                .NonLazy();    
            
            Container
                .Bind<HealthComponent>()
                .AsSingle()
                .WithArguments(_config.MaxHealth);
        }
    }
}