using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Data.Config
{
    [CreateAssetMenu(
        fileName = "TowerConfig",
        menuName = "Configs/Tower")]
    public class TowerConfig : ScriptableObject
    {
        [field: SerializeField] public List<StatsConfig> Stats { get; private set; }
        [field: SerializeField] public int InitialMoney { get; private set; }

        private void OnValidate()
        {
            // if (Stats.Count < 1)
            //     return;
            //
            // for (int i = 0; i < Stats.Count; i++)
            // {
            //     if (Stats[i].Type == Stats[i + 1].Type)
            //     {
            //         Debug.Log("+");
            //         Stats.RemoveAt(i);
            //     }
            // }
        }
    }
}