using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Tower.Animation.AnimationSettings
{
    [System.Serializable]
    public struct TowerDeathSettings
    {
        [field: SerializeField] public float DurationDoScale { get; private set; }
        [field: SerializeField] public Ease EaseType { get; private set; }
    }
}