using _Project.Cor.Enemy.Mono;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Cor.Enemy
{
    [UsedImplicitly]
    public class AnimationEnemy
    {
        private const float _durationAnimation = 1f;
        private const float _strength = 0.25f;
        
        private readonly Transform _transform;

        public AnimationEnemy(EnemyView view)
        {
            _transform = view.RotationTransform;
        }

        public void PlayTakeDamageAnimation() => 
            _transform.DOShakePosition(_durationAnimation,_strength).Play();
    }
}