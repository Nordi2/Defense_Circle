using System;
using _Project.Scripts.Gameplay.Component;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    [UsedImplicitly]
    public class AnimationTower
    {
        private const float DurationShakeCamera = 1f;
        private const float DurationSwitchColor = 0.25f;
        private const float StrengthShake = 0.5f;
        private const float DurationDoScale = 1f;
        private const float DurationAnimationDoFadeHealthText = 0.5f;

        private const int CountLoops = 4;

        private Sequence _sequence;

        private readonly Transform _transformParent;
        private readonly Color _normalColor;
        private readonly Color _takeDamageColor;
        private readonly Camera _camera;
        private readonly SpriteRenderer _spriteSwitchColor;
        private readonly HealthView _healthView;

        public AnimationTower(
            TowerView view,
            Camera camera,
            HealthView healthView)
        {
            _spriteSwitchColor = view.SpriteSwitchColor;
            _normalColor = view.NormalColor;
            _takeDamageColor = view.TakeDamageColor;
            _transformParent = view.transform;
            _camera = camera;
            _healthView = healthView;
        }

        public void PlayInitialSpawn(Action initialSpawnAction)
        {
            _sequence = DOTween.Sequence();

            _sequence
                .AppendCallback(() => initialSpawnAction?.Invoke())
                .Append(_transformParent
                    .DOScale(Vector3.one, DurationDoScale)
                    .From(Vector3.zero)
                    .SetEase(Ease.OutBounce))
                .Append(TextDoFadeAnimation(_healthView.LabelText, DurationAnimationDoFadeHealthText))
                .Join(TextDoFadeAnimation(_healthView.CurrentHealthText, DurationAnimationDoFadeHealthText))
                .Join(TextDoFadeAnimation(_healthView.SeparatorText, DurationAnimationDoFadeHealthText))
                .Join(TextDoFadeAnimation(_healthView.MaxHealthText, DurationAnimationDoFadeHealthText));
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
            _transformParent
                .DOScale(Vector3.zero, DurationDoScale)
                .From(Vector3.one)
                .SetEase(Ease.OutBounce);
        }
        
        private Tween TextDoFadeAnimation(TextMeshProUGUI text, float duration)
        {
            return text
                .DOFade(1, duration)
                .From(0);
        }
    }
}