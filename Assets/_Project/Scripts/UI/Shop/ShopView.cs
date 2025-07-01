using _Project.Data.Config;
using _Project.Scripts.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.Shop
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private UpgradeCartView _cartViewPrefab;
        [SerializeField] private Transform _containerUpgradeCarts;
        [SerializeField] private RectTransform _moveContainer;
        [SerializeField] private AnimationSettingsShop _animationSettings;
        [SerializeField] private Image _antiClicker;
        
        private Vector2 _originalPosition;
        private Vector2 _hiddenPosition;

        private Sequence _openSequence;
        private Sequence _closeSequence;

        private void Awake()
        {
            CreateHiddenAndOriginalPosition();
            AnimationShopCache();
        }

        public void OpenShop() =>
            _openSequence.Restart();

        public void CloseShop() =>
            _closeSequence.Restart();

        public UpgradeCartView SpawnCart() =>
            Instantiate(_cartViewPrefab, _containerUpgradeCarts);

        public void UpdateAmountMoney(string value) =>
            _moneyText.text = value;

        private void AnimationShopCache()
        {
            _openSequence = DOTween.Sequence()
                .AppendCallback(() => gameObject.SetActive(true))
                .Append(_animationSettings.AnimationOpenShop(_moveContainer, _originalPosition))
                .Join(_animationSettings.AnimationAntiClickerShow(_antiClicker))
                .SetAutoKill(false);

            _closeSequence = DOTween.Sequence()
                .Append(_animationSettings.AnimationCloseShop(_moveContainer, _hiddenPosition))
                .Join(_animationSettings.AnimationAntiClickerHide(_antiClicker))
                .AppendCallback(() => gameObject.SetActive(false))
                .SetAutoKill(false);
        }

        private void CreateHiddenAndOriginalPosition()
        {
            _originalPosition = _moveContainer.anchoredPosition;
            _hiddenPosition = new Vector2(_originalPosition.x, -Screen.height);
            _moveContainer.anchoredPosition = _hiddenPosition;
        }
    }
}