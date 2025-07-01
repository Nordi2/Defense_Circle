using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Data.Config
{
    [CreateAssetMenu(
        fileName = "AnimationSettingsMenu",
        menuName = "Configs/Animation/AnimationSettingsMenu")]
    public class AnimationSettingsMenu : ScriptableObject
    {
        [Header("Animation Anti-clicker")] 
        [SerializeField] private Vector2 _endValueAlphaShowHide;
        [SerializeField] private Vector2 _antiClickerDurationShowHide;
        [SerializeField] private Ease _easeAntiClicker = Ease.Linear;

        [Header("Animation Panel")]
        [SerializeField] private Vector2 _panelDurationShowHide;
        [SerializeField] private Ease _easePanel = Ease.OutBack;

        [Header("Animation Button")] 
        [SerializeField] private Ease _easeButton = Ease.OutBack;
        [SerializeField] private Vector2 _buttonDurationShowHide;
        [SerializeField] private Vector2 _endValueAlphaButtonShowHide;

        public Tween AnimationAntiClickerShow(Image image) =>
            image
                .DOFade(_endValueAlphaShowHide.x, _antiClickerDurationShowHide.x)
                .From(0)
                .SetEase(_easeAntiClicker);

        public Tween AnimationAntiClickerHide(Image image) =>
            image
                .DOFade(_endValueAlphaShowHide.y, _antiClickerDurationShowHide.y)
                .From(_endValueAlphaShowHide.x)
                .SetEase(_easeAntiClicker);

        public Tween AnimationPanelShow(RectTransform panel, Vector2 position) =>
            panel
                .DOAnchorPos(position, _panelDurationShowHide.x)
                .SetEase(_easePanel);

        public Tween AnimationPanelHide(RectTransform panel, Vector2 position) =>
            panel
                .DOAnchorPos(position, _panelDurationShowHide.y)
                .SetEase(_easePanel);

        public Sequence AnimationButtonShow(List<Button> buttons)
        {
            Sequence buttonAnimationSequenceShow = DOTween.Sequence();

            for (int i = 0; i < buttons.Count; i++)
            {
                buttonAnimationSequenceShow
                    .Append(buttons[i].GetComponent<CanvasGroup>()
                        .DOFade(_endValueAlphaButtonShowHide.x, _buttonDurationShowHide.x)
                        .From(0)
                        .SetEase(_easeButton));
            }

            return buttonAnimationSequenceShow;
        }

        public Sequence AnimationButtonHide(List<Button> buttons)
        {
            Sequence buttonAnimationSequenceHide = DOTween.Sequence();

            for (int i = 1; i > buttons.Count + 1; i--)
            {
                buttonAnimationSequenceHide
                    .Append(buttons[i].GetComponent<CanvasGroup>()
                        .DOFade(_endValueAlphaButtonShowHide.y, _buttonDurationShowHide.y)
                        .From(_endValueAlphaButtonShowHide.x)
                        .SetEase(_easeButton));
            }

            return buttonAnimationSequenceHide;
        }
    }
}