using _Project.Scripts.Data;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.Services.Data
{
    [UsedImplicitly]
    public class DataService : 
        IDataService, 
        IInitializable
    {
        private TowerConfig _towerConfig;

        void IInitializable.Initialize() => 
            _towerConfig = Resources.Load<TowerConfig>("Data/TowerConfig");

        public TowerConfig GetTowerConfig() => 
            _towerConfig;
    }
}