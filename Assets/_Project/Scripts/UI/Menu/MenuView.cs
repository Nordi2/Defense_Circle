using System.Collections.Generic;
using _Project.Data.Config;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class MenuView : MonoBehaviour
    {
        [field: SerializeField] public Button ContinueButton { get; private set; }
        [field: SerializeField] public Button ShopButton { get; private set; }
        
        [SerializeField] private Image _antiClickerImage;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private List<Button> _buttons;
        [SerializeField] private AnimationSettingsMenu _animationSettings;
        
        private Vector2 _originalPosition;
        private Vector2 _hiddenPosition;

        private Tween _animationAntiClicker;
        private Tween _animationShowPanel;
        private Sequence _buttonShowSequence;
        private Sequence _openMenuSequence;

        private void Awake()
        {
            _originalPosition = _rectTransform.anchoredPosition;
            _hiddenPosition = new Vector2(_originalPosition.x, -Screen.height);
            _rectTransform.anchoredPosition = _hiddenPosition;

            AnimationShowPanelCache();
            AnimationAntiClickerCache();
            AnimationButtonShowCache();
            AnimationOpenMenuCache();
        }

        public void Show()
        {
            for (int i = 0; i < _buttons.Count; i++)
                _buttons[i].interactable = false;
            
            _openMenuSequence.Restart();
        }

        public void Hide()
        {
            for (int i = 0; i < _buttons.Count; i++)
                _buttons[i].interactable = false;
            
            _openMenuSequence.PlayBackwards();
        }

        private void AnimationOpenMenuCache()
        {
            _openMenuSequence = DOTween.Sequence()
                .AppendCallback(() => gameObject.SetActive(true))
                .Append(_animationShowPanel)
                .Join(_animationAntiClicker)
                .Append(_buttonShowSequence)
                .AppendCallback(() => _buttons.ForEach(button => button.interactable = true))
                .SetAutoKill(false);
        }

        private void AnimationButtonShowCache()
        {
            _buttonShowSequence = DOTween.Sequence()
                .SetAutoKill(false);

            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttonShowSequence
                    .Append(_buttons[i].GetComponent<CanvasGroup>()
                        .DOFade(_animationSettings.EndValueAlphaButton, _animationSettings.ButtonAnimationDuration)
                        .From(0)
                        .SetEase(_animationSettings.EaseButton));
            }
        }

        private void AnimationAntiClickerCache()
        {
            _animationAntiClicker = _antiClickerImage
                .DOFade(_animationSettings.EndValueAlpha, _animationSettings.AntiClickerAnimationDuration)
                .From(0)
                .SetAutoKill(false)
                .SetEase(_animationSettings.EaseAntiClicker);
        }

        private void AnimationShowPanelCache()
        {
            _animationShowPanel = _rectTransform
                .DOAnchorPos(_originalPosition, _animationSettings.AnimationPanelDuration)
                .SetEase(_animationSettings.EasePanel)
                .SetAutoKill(false);
        }
    }
}