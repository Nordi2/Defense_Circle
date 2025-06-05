using _Project.Scripts.Infrastructure.Services.GameLoop;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class InitialTextLoadAfterLoading : MonoBehaviour ,
        IGameStartListener
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private TextMeshProUGUI _textPressSpace;
        
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _endScaleValue;

        private Sequence _sequence;

        public RectTransform RectTransform => _rectTransform;

        public void StartAnimation()
        {
            _sequence = DOTween.Sequence();

            _sequence
                .Append(_textPressSpace.DOFade(0, _animationDuration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo))
                .Join(_textPressSpace.DOScale(_endScaleValue, _animationDuration).SetLoops(-1, LoopType.Yoyo))
                .Play();
        }
        
        public void OnGameStart()
        {
            _sequence.Kill();
            gameObject.SetActive(false);
        }
    }
}