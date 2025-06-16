using Data.Tables.Parent;
using UnityEngine;

namespace Data.Tables
{
    [System.Serializable]
    public class CountTargetsTables : StatsTables
    {
        public override float GetValue(int currentLevel)
        {
            var value = StatsValue[currentLevel - 1];
            
            return Mathf.RoundToInt(value);
        }

        public override void OnValidate(int maxLevel)
        {
            StatsValue = new float[maxLevel];

            for (int i = 0; i < StatsValue.Length; i++)
            {
                var value = InitialValue + i;

                StatsValue[i] = value;
            }
        }
    }
}