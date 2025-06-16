using _Project.Scripts.Gameplay.Stats;
using UnityEngine;

namespace _Project.Scripts.Data
{
    public abstract class StatsConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField, Min(1)] public int InitialLevel { get; private set; }
        [field: SerializeField] public int MaxLevel { get; private set; }
        [field: SerializeField] public StatsType StatsType { get; private set; } = StatsType.None;
        [field: SerializeField] public PriceTable PriceTable { get; private set; }
    }
}