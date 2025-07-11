using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Component
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentAmountText;
        [SerializeField] private TextMeshProUGUI _maxAmountText;
        
        [Header("Animation-Settings")] 
        [SerializeField] private float _animationDuration = 0.2f;
        [SerializeField] private Ease _ease = Ease.Linear;
    
        private Tween _tween;
        
        public void UpdateCurrentHealthText(int oldValue, int newValue, bool isCache = false)
        {
            _tween?.Kill();

            if (isCache)
            {
                _tween.Restart();
            }
            else
            {
                _currentAmountText
                    .DOCounter(oldValue,newValue,_animationDuration)
                    .SetEase(_ease)
                    .SetAutoKill(false)
                    .Play();
            }
        }
    }
}