using Cor.Enemy.Mono;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Cor.Enemy
{
    [UsedImplicitly]
    public class AnimationEnemy
    {
        private readonly Transform _transform;

        public AnimationEnemy(EnemyView view)
        {
            _transform = view.RotationTransform;
        }

        public void PlayTakeDamageAnimation()
        {
            //_transform.DOShakeScale(1f);
            _transform.DOShakePosition(1,0.25f);
        }
    }
}