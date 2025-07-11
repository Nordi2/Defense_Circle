using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Data.Config
{
    [CreateAssetMenu(
        fileName = "AnimationSettingsShop",
        menuName = "Configs/Animation/AnimationSettingsShop")]
    public class AnimationSettingsShop : ScriptableObject
    {
        [Header("Animation Move Panel")] 
        [SerializeField] private float _durationOpen;
        [SerializeField] private Ease _easeOpen;

        [SerializeField] private float _durationClose;
        [SerializeField] private Ease _easeClose;

        [Header("Animation Anti-clicker")] 
        [SerializeField] private Vector2 _endValueAlphaShowHide;
        [SerializeField] private Vector2 _antiClickerDurationShowHide;
        [SerializeField] private Ease _easeAntiClicker = Ease.Linear;

        public Tween AnimationOpenShop(RectTransform panel, Vector2 position) =>
            panel
                .DOAnchorPos(position, _durationOpen)
                .SetEase(_easeOpen);

        public Tween AnimationCloseShop(RectTransform panel, Vector2 position) =>
            panel
                .DOAnchorPos(position, _durationClose)
                .SetEase(_easeClose);

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
    }
}