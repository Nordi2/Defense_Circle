using UnityEngine;

namespace _Project.Scripts.Gameplay.Stats
{
    [System.Serializable]
    public class PriceTable
    {
        [SerializeField] private int _initialPrice;
        [SerializeField] private int _step;
        [SerializeField] private int[] _priceValue;

        public int GetValue(int currentLevel) =>
            _priceValue[currentLevel - 1];

        public void OnValidate(int maxLevel)
        {
            _priceValue = new int[maxLevel];

            for (int i = 0; i < _priceValue.Length; i++)
            {
                int value = _initialPrice + _step * i;
                _priceValue[i] = value;
            }
        }
    }
}