using _Project.Meta.StatsLogic;
using UnityEngine;

namespace _Project.Data.Config.Stats
{
    [CreateAssetMenu(
        fileName = "StatsConfig",
        menuName = "Configs/Stats")]
    public class StatsConfig : ScriptableObject
    {
        [field: SerializeField, Min(3)] public int MaxLevel { get; private set; }
        [field: SerializeField] public int InitialLevel { get; private set; }
        [field: SerializeField] public StatsView View { get; private set; }
        [field: SerializeField] public StatsType Type { get; private set; } = StatsType.None;
        [field: SerializeField] public PriceTables PriceTables {get; private set; }
        [field: SerializeField] public ValueTables ValueTables {get; private set; }
        
        [SerializeField, Min(50)] private int _initialPrice;
        
        private void OnValidate()
        {
            InitialLevel = Mathf.Clamp(InitialLevel, 1, MaxLevel);
            
            PriceTables.CreatePriceTable(MaxLevel);    
            ValueTables.CreateValueTable(MaxLevel);
        }
    }
}