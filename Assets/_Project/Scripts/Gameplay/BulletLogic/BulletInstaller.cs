using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.BulletLogic
{
    public class BulletInstaller : MonoInstaller
    {
        [SerializeField] private Transform _bulletTransform;
        [SerializeField] private float _moveSpeed;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<BulletMovement>()
                .AsSingle()
                .WithArguments(_moveSpeed,_bulletTransform);
        }
    }
}