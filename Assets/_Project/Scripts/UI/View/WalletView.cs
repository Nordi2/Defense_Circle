using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.View
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _allText;
        [SerializeField] private TextMeshProUGUI _moneyText;

        [SerializeField] private float _animationDuration;
        [SerializeField] private Ease _ease;

        private Tween _tween;

        public void StartAnimation()
        {
            for (int i = 0; i < _allText.Length; i++)
            {
                _allText[i]
                    .DOFade(1, 0)
                    .From(0);
            }
        }

        public void UpdateCurrentMoneyText(int oldValue, int newValue)
        {
            if (_tween.IsActive())
                _tween.Kill();

            _tween = _moneyText
                .DOCounter(oldValue, newValue, _animationDuration)
                .SetEase(_ease);
        }
    }
}