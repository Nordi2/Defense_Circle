using UnityEngine;

namespace _Project.Scripts.Gameplay.Tower.Animation.AnimationSettings
{
    [System.Serializable]
    public struct TowerTakeDamageSettings
    {
        [field: SerializeField] public SpriteRenderer[] AnimationSpriteRenderers { get; private set; }
        [field: SerializeField] public float DurationShakeCamera { get; private set; }
        [field: SerializeField] public float StrengthShakeCamera { get; private set; }
        [field: SerializeField] public float DurationSwitchColor { get; private set; }
        [field: SerializeField] public int CountLoops { get; private set; }
        [field: SerializeField] public Color TakeDamageColor { get; private set; }
        [field: SerializeField] public Color NormalColor { get; private set; }
    }
}