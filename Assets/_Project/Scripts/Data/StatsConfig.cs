using _Project.Scripts.Gameplay.Stats;
using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(
        fileName = "StatsConfig",
        menuName = "Configs/Shop/StatsConfig")]
    public class StatsConfig : ScriptableObject
    {
        [field: SerializeField, Min(2)] public int MaxLevelValue { get; private set; }
        [field: SerializeField] public string NameStats { get; private set; }
        [field: SerializeField] public Sprite IconStats { get; private set; }
        [field: SerializeField] public StatsType StatsType { get; private set; } = StatsType.None;

        [field: SerializeField] private int _initialValue;
        [field: SerializeField] private int[] _value;
        [field: SerializeField] private int[] _priceValue;

        private void OnValidate()
        {
            InitializeArray(ref _value);
            InitializeArray(ref _priceValue);

            GeneratePrice(_priceValue);
            GenerateValues(_value);
        }

        public int GetValues(int level) => 
            _value[level];

        public int GetPrice(int level) => 
            _priceValue[level];

        private void InitializeArray(ref int[] array)
        {
            if (array == null || array.Length != MaxLevelValue)
                array = new int[MaxLevelValue];
        }

        private void GenerateValues(int[] arrayValue)
        {
            if(arrayValue.Length == MaxLevelValue)
                return;
            
            _value[0] = _initialValue;
            for (int i = 1; i < MaxLevelValue; i++)
            {
                int addValue = (int)(arrayValue[i] * 0.5f);
                arrayValue[i] = i * _initialValue + addValue;
            }
        }

        private void GeneratePrice(int[] arrayPrice)
        {
            if(arrayPrice.Length == MaxLevelValue)
                return;
            
            float basePrice = 200f;
            for (int i = 0; i < MaxLevelValue; i++)
            {
                float noise = Random.Range(0.1f, 0.75f);
                arrayPrice[i] = (int)(i * basePrice + basePrice * noise);
            }
        }
    }
}