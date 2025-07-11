using System;
using UnityEngine;

namespace _Project.Data.Config.Stats
{
    [Serializable]
    public class PriceTables
    {
        [field: SerializeField] public int[] PriceTable { get; private set; }

        public void CreatePriceTable(int arrayLength)
        {
            if (PriceTable.Length != arrayLength)
                PriceTable = new int[arrayLength];
        }

        public int GetPrice(int level)
        {
            level -= 1;

            return PriceTable[level];
        }
    }
}