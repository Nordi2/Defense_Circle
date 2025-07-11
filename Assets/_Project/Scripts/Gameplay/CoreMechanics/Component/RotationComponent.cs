using _Project.Cor.Enemy.Mono;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Cor.Component
{
    [UsedImplicitly]
    public class RotationComponent :
        ITickable
    {
        private readonly float _rotationSpeed;
        private readonly Transform _rotateTransform;

        public RotationComponent(
            EnemyView view,
            float rotationSpeed)
        {
            _rotateTransform = view.RotationTransform;
            _rotationSpeed = rotationSpeed;
        }
        
        void ITickable.Tick() => 
            _rotateTransform.Rotate(_rotateTransform.forward, _rotationSpeed * Time.deltaTime);
    }
}