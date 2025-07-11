using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class Curtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _animationDuration;

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            _canvasGroup
                .DOFade(0, _animationDuration)
                .From(1)
                .OnComplete(() => gameObject.SetActive(false))
                .Play();
        }
    }
}