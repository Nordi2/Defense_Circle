using DG.Tweening;
using UnityEngine;

namespace _Project.Data.Config
{
    [CreateAssetMenu(
        fileName = "AnimationSettingsMenu",
        menuName = "Configs/Animation/AnimationSettingsMenu")]
    public class AnimationSettingsMenu : ScriptableObject
    {
        [field: Header("Animation Anti-clicker")]
        [field: SerializeField] public float AntiClickerAnimationDuration { get; private set; } = 0.5f;
        [field: SerializeField] public Ease EaseAntiClicker { get; private set; } = Ease.Linear;
        [field: SerializeField, Range(0, 1)] public float EndValueAlpha { get; private set; } = 0.4f;
        [field: Header("Animation Panel")]
        [field: SerializeField] public float AnimationPanelDuration { get; private set; } = 0.5f;
        [field: SerializeField] public Ease EasePanel { get; private set; } = Ease.OutBack;
        [field: Header("Animation Button")]
        [field: SerializeField] public Ease EaseButton { get; private set; } = Ease.OutBack;
        [field: SerializeField] public float ButtonAnimationDuration { get; private set; }
        [field: SerializeField,Range(0,1)] public float EndValueAlphaButton { get; private set; }
    }
}