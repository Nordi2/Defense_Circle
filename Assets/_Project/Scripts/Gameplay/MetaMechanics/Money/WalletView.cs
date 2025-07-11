using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Meta.Money
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentAmountText;

        [Header("Animation Settings")] 
        [SerializeField] private float _animationDuration;
        [SerializeField] private Ease _ease;

        private Tween _tweenCurrentAmount;

        public void UpdateCurrentMoneyAmount(int oldValue, int newValue, bool isCache = false)
        {
            _tweenCurrentAmount?.Kill();

            if (isCache)
                _tweenCurrentAmount.Restart();
            else
                _tweenCurrentAmount = AnimationText(_currentAmountText, oldValue, newValue);
        }

        private Tween AnimationText(TextMeshProUGUI text, int oldValue, int newValue)
        {
            return text
                .DOCounter(oldValue, newValue, _animationDuration)
                .SetEase(_ease)
                .SetAutoKill(false)
                .Play();
        }
    }
}