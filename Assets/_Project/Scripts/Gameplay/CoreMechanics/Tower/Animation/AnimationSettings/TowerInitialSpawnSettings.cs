using DG.Tweening;
using UnityEngine;

namespace _Project.Cor.Tower.Animation.AnimationSettings
{
    [System.Serializable]
    public struct TowerInitialSpawnSettings
    {
        [field: SerializeField] public float DurationDoFade { get; private set; }
        [field: SerializeField] public float DurationDoScale { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
    }
}