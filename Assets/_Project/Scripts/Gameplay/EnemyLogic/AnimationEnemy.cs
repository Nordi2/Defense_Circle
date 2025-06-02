using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.EnemyLogic
{
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