namespace _Project.Scripts.Data
{
    [System.Serializable]
    public class HealthTable : StatsTables
    {
        public override float GetValue(int currentLevel) => 
            StatsValue[currentLevel - 1];

        public override void OnValidate(int maxLevel)
        {
            StatsValue = new float[maxLevel];

            for (int i = 0; i < StatsValue.Length; i++)
            {
                float value = InitialValue - (i * Step);

                StatsValue[i] = value;
            }
        }
    }
}