using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class ShopUpgrade : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private UpgradeCartView _upgradeCartPrefab;
        [SerializeField] private AnimationCurve _animationCurve;
        
        private List<UpgradeCartView> _upgradeCarts = new();
        private Sequence _animationSequence;
        
        public RectTransform RectTransform => _rectTransform;

        private void Awake()
        {
            for (int i = 0; i < 3; i++)
            {
                UpgradeCartView upgradeCart = Instantiate(_upgradeCartPrefab, _gridLayoutGroup.transform);
                _upgradeCarts.Add(upgradeCart);
            }
        }

        public void OpenShop()
        {
            Show();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Show()
        {
            /*_animationSequence = DOTween.Sequence();

            for (int i = 0; i < _upgradeCarts.Count; i++)
            {
                // _upgradeCarts[i].UpdateIcon(_shopItemStorage.StatsConfigs[StatsType.Health].IconStats);
                // _upgradeCarts[i].UpdateNameText(_shopItemStorage.StatsConfigs[StatsType.Health].NameStats);
                // _upgradeCarts[i]
                //     .UpdateMoneyText(
                //         $"Money: {_shopItemStorage.StatsConfigs[StatsType.Health].GetPrice(1).ToString()}$");
            }

            _animationSequence
                .AppendCallback(() => gameObject.SetActive(true))
                .Append(_image.DOFade(0.5f, 0.5f).From(0))
                .Append(AnimationUpgradeCart());*/
        }

        // private Sequence AnimationUpgradeCart()
        // {
        //     Sequence sequence = DOTween.Sequence();
        //
        //     sequence
        //         .Append(_upgradeCarts[0].transform.DOScale(Vector3.one, 0.5f).From(0).SetEase(_animationCurve))
        //         .Append(_upgradeCarts[1].transform.DOScale(Vector3.one, 0.5f).From(0).SetEase(_animationCurve))
        //         .Append(_upgradeCarts[2].transform.DOScale(Vector3.one, 0.5f).From(0).SetEase(_animationCurve));
        //
        //     return sequence;
        // }

        // public void Hide(bool isFirst = false)
        // {
        //     if (isFirst)
        //     {
        //         gameObject.SetActive(false);
        //         return;
        //     }
        //
        //     _image.DOFade(0, 1).From(1).OnComplete(() => gameObject.SetActive(false));
        // }
    }
}