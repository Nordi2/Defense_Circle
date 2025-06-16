using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Component
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _maxHealthText;
        [SerializeField] private TextMeshProUGUI _currentHealthText;
        [SerializeField] private TextMeshProUGUI _labelText;
        [SerializeField] private TextMeshProUGUI _separatorText;
        
        [Header("Animation-Settings")] 
        [SerializeField] private float _animationDuration = 0.2f;
        [SerializeField] private Ease _ease = Ease.Linear;
    
        private Tween _tween;

        public TextMeshProUGUI[] AllHealthText
        {
            get
            {
                return new[]
                {
                    _maxHealthText,
                    _currentHealthText,
                    _labelText,
                    _separatorText
                };
            }
        }
        
        public void UpdateMaxHealthText(int newValueText)
        {
            _maxHealthText.text = newValueText.ToString();
        }

        public void UpdateCurrentHealthText(int oldValue, int newValue)
        {
            if (_tween.IsActive())
                _tween.Kill();
            
            _tween = _currentHealthText
                .DOCounter(oldValue, newValue, _animationDuration)
                .SetEase(_ease);
        }
    }
}