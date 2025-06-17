using System;
using _Project.Data.Tables.Parent;
using UnityEngine;

namespace _Project.Meta.Stats
{
    public abstract class Stats
    {
        public event Action<Stats> OnUpgradeStats;
        public int CurrentLevel { get; set; }
        public int MaxLevel { get; set; }
        public int Price { get; set; }
        public Sprite Icon { get; set; }
        public float ValueStats { get; set; }
        public PriceTable PriceTable { get; set; }
        public StatsTables StatsTables { get; set; }
        
        private StatsTables _statsTables;
        private PriceTable _priceTable;
        
        public bool IsMaxLevel => CurrentLevel >= MaxLevel;

        public void UpgradeStats()
        {
            CurrentLevel++;
            ValueStats = StatsTables.GetValue(CurrentLevel);
            Price = PriceTable.GetValue(CurrentLevel);
            
            OnUpgradeStats?.Invoke(this);
        }
    }
}