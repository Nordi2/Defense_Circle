using System;
using _Project.Scripts.Gameplay.Component;
using _Project.Scripts.Gameplay.Tower;
using _Project.Scripts.Gameplay.Tower.Animation.AnimationSettings;
using Cor.Tower.Mono;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Cor.Tower
{
    [UsedImplicitly]
    public class AnimationTower
    {
        private Sequence _sequence;

        private readonly HealthView _healthView;
        private readonly Transform _mainTransform;
        private readonly Camera _camera;
        
        private readonly TowerInitialSpawnSettings _initialSpawnSettings;
        private readonly TowerTakeDamageSettings _takeDamageSettings;
        private readonly TowerDeathSettings _deathSettings;
        
        public AnimationTower(
            TowerView view,
            Camera camera)
        {
            _camera = camera;
            _healthView = view.HealthView;
            _mainTransform = view.transform;
            
            _initialSpawnSettings = view.AnimationSpawnSettings;
            _takeDamageSettings = view.AnimationTakeDamageSettings;
            _deathSettings = view.AnimationDeathSettings;
        }

        private float _durationShake =>
            _takeDamageSettings.DurationShakeCamera;

        private float _strengthShake =>
            _takeDamageSettings.StrengthShakeCamera;

        private float _durationSwitchColor =>
            _takeDamageSettings.DurationSwitchColor;

        private int _countLoops =>
            _takeDamageSettings.CountLoops;

        private Color _takeDamageColor =>
            _takeDamageSettings.TakeDamageColor;

        private Color _normalColor =>
            _takeDamageSettings.NormalColor;

        public void PlayInitialSpawn(Action initialSpawnAction = null)
        {
            _sequence = DOTween.Sequence();

            _sequence
                .AppendCallback(() => initialSpawnAction?.Invoke())
                .Append(_mainTransform
                    .DOScale(Vector3.one, _initialSpawnSettings.DurationDoScale)
                    .From(Vector3.zero)
                    .SetEase(_initialSpawnSettings.Ease))
                .Append(GetTextFadeSequence());
        }

        public void PlayTakeDamage()
        {
            if (_sequence.IsActive())
                _sequence.Kill();

            _sequence = DOTween.Sequence();

            _sequence
                .Append(_camera.DOShakePosition(_durationShake, _strengthShake))
                .Join(AnimationSprites())
                .OnKill(KillCallback);
        }

        public void PlayDeath()
        {
            _mainTransform
                .DOScale(Vector3.zero, _deathSettings.DurationDoScale)
                .From(Vector3.one)
                .SetEase(_deathSettings.EaseType);
        }

        private Sequence GetTextFadeSequence()
        {
            Sequence textSequence = DOTween.Sequence();
            
            foreach (TextMeshProUGUI text in _healthView.AllHealthText)
            {
                Color originalColor = text.color;
                text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
                
                textSequence.Join(
                    text.DOFade(1f, _initialSpawnSettings.DurationDoFade)
                );
            }
    
            return textSequence;
        }

        private Sequence AnimationSprites()
        {
            Sequence spriteSequence = DOTween.Sequence();
            
            for (int i = 0; i < _takeDamageSettings.AnimationSpriteRenderers.Length; i++)
            {
                spriteSequence
                    .Join(_takeDamageSettings.AnimationSpriteRenderers[i]
                        .DOColor(_takeDamageColor,_durationSwitchColor)
                        .SetLoops(_countLoops, LoopType.Yoyo));
            }

            return spriteSequence;
        }

        private void KillCallback()
        {
            for (int i = 0; i < _takeDamageSettings.AnimationSpriteRenderers.Length; i++)
                _takeDamageSettings.AnimationSpriteRenderers[i].color = _normalColor;
        }
    }
}