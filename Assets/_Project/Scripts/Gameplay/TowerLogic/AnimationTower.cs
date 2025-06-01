using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    [UsedImplicitly]
    public class AnimationTower : 
        IInitializable
    {
        private Camera _camera;
        private Sequence _sequence;
        private Tween _tween;
        private readonly SpriteRenderer[] _sprites;
        private Color _normalColor = new Color(1f,0.42f,0f);
        
        public AnimationTower(SpriteRenderer[] sprites)
        {
            _sprites = sprites;
        }
        
        void IInitializable.Initialize()
        {
            _camera = Camera.main;
        }

        public void PlayAnimationTakeDamage()
        {
            if (_sequence.IsActive()) 
                _sequence.Kill();

            _sequence = DOTween.Sequence();
            
            _sequence
                .Append(_camera.DOShakePosition(1f,0.5f))
                .Join(_sprites[0].DOColor(Color.red, 0.25f).SetLoops(4,LoopType.Yoyo))
                .OnKill(() => _sprites[0].color = _normalColor);
        }
    }
}