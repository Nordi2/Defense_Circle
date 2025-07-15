using System;
using _Project.Cor.Tower.Animation.AnimationSettings;
using _Project.Cor.Tower.Mono;
using _Project.Scripts.Gameplay.Component;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Cor.Tower.Animation
{
    [UsedImplicitly]
    public class AnimationTower
    {
        private const float _multiplier = 0.02f;
        
        private Sequence _sequence;
        
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
            _mainTransform = view.transform;
            
            _initialSpawnSettings = view.AnimationSpawnSettings;
            _takeDamageSettings = view.AnimationTakeDamageSettings;
            _deathSettings = view.AnimationDeathSettings;
        }
        
        public void PlayInitialSpawn(Action initialSpawnAction = null)
        {
            _sequence = DOTween.Sequence();

            _sequence
                .AppendCallback(() => initialSpawnAction?.Invoke())
                .Append(_mainTransform
                    .DOScale(Vector3.one, _initialSpawnSettings.DurationDoScale)
                    .From(Vector3.zero)
                    .SetEase(_initialSpawnSettings.Ease))
                .Play();
        }

        public void PlayTakeDamage(float damage)
        {
            if (_sequence.IsActive())
                _sequence.Kill();
            
            _sequence = DOTween.Sequence();
            
            _sequence
                .Append(_camera.DOShakePosition( _takeDamageSettings.DurationShakeCamera, damage * _multiplier))
                .Join(AnimationSprites())
                .OnKill(KillCallback)
                .Play();
        }

        public void PlayDeath()
        {
            _mainTransform
                .DOScale(Vector3.zero, _deathSettings.DurationDoScale)
                .From(Vector3.one)
                .SetEase(_deathSettings.EaseType)
                .Play();
        }
        
        private Sequence AnimationSprites()
        {
            Sequence spriteSequence = DOTween.Sequence();
            
            for (int i = 0; i < _takeDamageSettings.AnimationSpriteRenderers.Length; i++)
            {
                spriteSequence
                    .Join(_takeDamageSettings.AnimationSpriteRenderers[i]
                        .DOColor(_takeDamageSettings.TakeDamageColor,_takeDamageSettings.DurationSwitchColor)
                        .SetLoops(_takeDamageSettings.CountLoops, LoopType.Yoyo))
                    .Play();
            }

            return spriteSequence;
        }

        private void KillCallback()
        {
            for (int i = 0; i < _takeDamageSettings.AnimationSpriteRenderers.Length; i++)
                _takeDamageSettings.AnimationSpriteRenderers[i].color = _takeDamageSettings.NormalColor;
        }
    }
}