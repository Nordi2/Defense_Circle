using _Project.Data.Config;
using System.Collections.Generic;
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
        
        private Sequence _openMenuSequence;
        private Sequence _closeMenuSequence;
        
        private void Awake()
        {
            CreateHiddenAndOriginalPosition();
            AnimationMenuCache();
        }

        public void Show() => 
            _openMenuSequence.Restart();

        public void Hide() => 
            _closeMenuSequence.Restart();

        private void AnimationMenuCache()
        {
            _openMenuSequence = DOTween.Sequence()
                .AppendCallback(() => gameObject.SetActive(true))
                .AppendCallback(() => SetButtonsInteractable(false))
                .Append(_animationSettings.AnimationPanelShow(_rectTransform,_originalPosition))
                .Join(_animationSettings.AnimationAntiClickerShow(_antiClickerImage))
                .Append(_animationSettings.AnimationButtonShow(_buttons))
                .AppendCallback(() => SetButtonsInteractable(true))
                .SetAutoKill(false);

            _closeMenuSequence = DOTween.Sequence()
                .AppendCallback(() => SetButtonsInteractable(false))
                .Append(_animationSettings.AnimationButtonHide(_buttons))
                .Join(_animationSettings.AnimationAntiClickerHide(_antiClickerImage))
                .Append(_animationSettings.AnimationPanelHide(_rectTransform, _hiddenPosition))
                .OnComplete(() => gameObject.SetActive(false))
                .SetAutoKill(false);
        }

        private void SetButtonsInteractable(bool state)
        {
            for (int i = 0; i < _buttons.Count; i++)
                _buttons[i].interactable = state;
        }

        private void CreateHiddenAndOriginalPosition()
        {
            _originalPosition = _rectTransform.anchoredPosition;
            _hiddenPosition = new Vector2(_originalPosition.x, -Screen.height);
            _rectTransform.anchoredPosition = _hiddenPosition;
        }
    }
}