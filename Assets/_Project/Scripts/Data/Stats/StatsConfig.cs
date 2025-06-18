using _Project.Meta.StatsLogic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "StatsConfig",
    menuName = "Configs/Stats")]
public class StatsConfig : ScriptableObject
{
    [field: SerializeField, Min(3)] public int MaxLevel { get; private set; }
    [field: SerializeField] public int InitialLevel { get; private set; }
    [field: SerializeField] public StatsView View { get; private set; }
    [field: SerializeField] public StatsType Type { get; private set; } = StatsType.None;

    [SerializeField, Min(50)] private int _initialPrice;

    [SerializeField] private int[] _value;
    [SerializeField] private int[] _price;

    private void OnValidate()
    {
        InitialLevel = Mathf.Clamp(InitialLevel, 1, MaxLevel);

        InitializeArray(ref _price);
        InitializeArray(ref _value);

        //GeneratePrice(_price);
    }

    public int GetPrice(int level)
    {
        level -= 1;
        if (level >= MaxLevel)
            throw new System.Exception("Invalid level");

        return _price[level];
    }

    public int GetValue(int level)
    {
        level -= 1;
        
        if (level >= MaxLevel)
            throw new System.Exception("Invalid level");

        return _value[level];
    }

    private void InitializeArray(ref int[] array)
    {
        if (array == null || array.Length != MaxLevel)
            array = new int[MaxLevel];
    }

    // private void GeneratePrice(int[] arrayPrice)
    // {
    //     arrayPrice[0] = _initialPrice;
    //     for (int i = 1; i < MaxLevel; i++)
    //     {
    //         float noise = Random.Range(1, 5);
    //         arrayPrice[i] = (int)(i * _initialPrice + _initialPrice * noise);
    //     }
    // }
}