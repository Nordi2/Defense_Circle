using UnityEngine;

namespace _Project.Data.Config.Stats
{
    [System.Serializable]
    public class ValueTables
    {
        [field: SerializeField] public int[] ValueTable { get; private set; }

        public void CreateValueTable(int arrayLength)
        {
            if (ValueTable.Length != arrayLength)
                ValueTable = new int[arrayLength];
        }
        
        public int GetValue(int level)
        {
            level -= 1;
            
            return ValueTable[level];
        }
    }
}