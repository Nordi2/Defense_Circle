using UnityEngine;

[System.Serializable]
public struct StatsView
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
}