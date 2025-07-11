using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Data.Config
{
    [CreateAssetMenu(
        fileName = "AnimationSettingsWaveView",
        menuName = "Configs/Animation/AnimationSettingsWaveView")]
    public class AnimationSettingsWaveView : ScriptableObject
    {
        [Header("Animation Settings")] [SerializeField]
        private float _animationDuration = 5;

        [SerializeField] private float _durationDoFadeTimerToNextWave = 0.25f;
        [SerializeField] private int _endValue;
        [SerializeField] private Ease _ease;

        private int _fromValue;

        private void OnValidate()
        {
            _fromValue = (int)_animationDuration;
        }

        public Sequence AnimationTimerToNextWave(TextMeshProUGUI textAnimation, Action callback = null)
        {
            return DOTween.Sequence()
                .Append(ActivateAnimationTimerToNextWave(textAnimation))
                .Append(textAnimation
                    .DOCounter(_fromValue, _endValue, _animationDuration)
                    .SetEase(_ease))
                .Append(DeactivateAnimationTimerToNextWave(textAnimation))
                .OnComplete(callback.Invoke)
                .Play()
                .SetAutoKill(false);
        }

        private Tween DeactivateAnimationTimerToNextWave(TextMeshProUGUI textAnimation) =>
            textAnimation
                .DOFade(0, _durationDoFadeTimerToNextWave)
                .OnComplete(() => textAnimation.gameObject.SetActive(false));

        private Tween ActivateAnimationTimerToNextWave(TextMeshProUGUI textAnimation) =>
            textAnimation
                .DOFade(1, _durationDoFadeTimerToNextWave)
                .OnComplete(() => textAnimation.gameObject.SetActive(true));
    }
}