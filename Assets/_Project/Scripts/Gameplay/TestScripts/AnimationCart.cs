using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.TestScripts
{
    public class AnimationCart : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private GameObject _nothingCart;
        [SerializeField] private GameObject _upgradeCart;

        private Vector2 _targetBodyPosition;
        private Vector2 _startShift;


        private void Awake()
        {
            _targetBodyPosition = _container.anchoredPosition;
            _startShift = new Vector2(_targetBodyPosition.x, Screen.height / 2);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartAnimation();
            }
        }

        private void StartAnimation()
        {
            RectTransform cartNothing = Instantiate(_nothingCart, _container).GetComponent<RectTransform>();

            Sequence animation = DOTween.Sequence();

            animation.Append(cartNothing.DOLocalRotate(new Vector3(0, 180, 0), 0.5f));

            /*
            animation
                .Append(cartNothing
                    .DOAnchorPos(_targetBodyPosition, 0.5f)
                    .From(_startShift).SetEase(Ease.InOutBack))
                .AppendCallback(CreateCard);
        */
        }

        private void CreateCard()
        {
            // for (int i = 0; i < 3; i++)
            // {
            //     RectTransform updagradeCart = Instantiate(_upgradeCart, _container).GetComponent<RectTransform>();
            //
            //     updagradeCart.DOAnchorPos(new Vector2(1000, 240), 2f);
            //
            // }
        }
    }
}