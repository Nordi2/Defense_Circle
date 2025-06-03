using System;
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
        private const float DurationShakeCamera = 1f;
        private const float DurationSwitchColor = 0.25f;
        private const float StrengthShake = 0.5f;
        private const float DurationDoScale = 1f;
        
        private const int CountLoops = 4;

        private Sequence _sequence;
        private Tween _tweenInitialSpawn;

        private Transform _transformParent;
        private readonly Color _normalColor;
        private readonly Color _takeDamageColor;
        private readonly Camera _camera;
        private readonly SpriteRenderer _spriteSwitchColor;

        public AnimationTower(
            TowerView view,
            Camera camera)
        {
            _spriteSwitchColor = view.SpriteSwitchColor;
            _normalColor = view.NormalColor;
            _takeDamageColor = view.TakeDamageColor;
            _transformParent = view.transform;
            _camera = camera;
        }

        public Tween InitialSpawnAnimation { get; private set; }
        public Tween DeathAnimation;
        
        void IInitializable.Initialize()
        {
            InitialSpawnAnimation = PlayInitialSpawn();
        }

        private Tween PlayInitialSpawn()
        {
            return _transformParent
                .DOScale(Vector3.one, DurationDoScale)
                .From(Vector3.zero)
                .SetEase(Ease.OutBounce)
                .SetAutoKill(false)
                .Pause();
        }

        public void PlayTakeDamage()
        {
            if (_sequence.IsActive())
                _sequence.Kill();

            _sequence = DOTween.Sequence();

            _sequence
                .Append(_camera
                    .DOShakePosition(DurationShakeCamera, StrengthShake))
                .Join(_spriteSwitchColor
                    .DOColor(_takeDamageColor, DurationSwitchColor)
                    .SetLoops(CountLoops, LoopType.Yoyo))
                .OnKill(() => _spriteSwitchColor.color = _normalColor);
        }

        public void PlayDeath()
        {
            _tweenInitialSpawn.PlayBackwards();
            //_transformParent.DOScale(Vector3.zero, 1f).From(Vector3.one).SetEase(Ease.OutBounce);
        }
    }
}