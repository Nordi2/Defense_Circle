using DG.Tweening;
using Infrastructure.Services;
using TMPro;
using UnityEngine;

namespace _Project
{
    public class InitialTextLoadAfterLoading : MonoBehaviour,
        IGameStartListener
    {
        [SerializeField] private TextMeshProUGUI _textPressSpace;

        [SerializeField] private float _animationDuration;
        [SerializeField] private float _endScaleValue;

        private Sequence _sequence;
        
        public void StartAnimation()
        {
            _sequence = DOTween.Sequence();

            _sequence
                .Append(_textPressSpace.DOFade(0, _animationDuration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo))
                .Join(_textPressSpace.DOScale(_endScaleValue, _animationDuration).SetLoops(-1, LoopType.Yoyo))
                .OnKill(() => gameObject.SetActive(false))
                .Play();
        }

        void IGameStartListener.OnGameStart()
        {
            _sequence.Kill(true);
        }
    }
}