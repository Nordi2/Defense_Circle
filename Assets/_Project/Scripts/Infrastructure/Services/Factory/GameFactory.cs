using _Project.Static;
using Cor.Interfaces;
using Cor.Tower.Mono;
using Infrastructure.Services;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Services
{
    [UsedImplicitly]
    public class GameFactory :
        IGameFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IGetTargetPosition _getTargetPosition;
        private readonly GameLoopService _gameLoopService;

        public GameFactory(
            IInstantiator instantiator,
            IGetTargetPosition getTargetPosition,
            GameLoopService gameLoopService)
        {
            _instantiator = instantiator;
            _getTargetPosition = getTargetPosition;
            _gameLoopService = gameLoopService;
        }

        public TowerFacade CreateTower()
        {
            GameObject towerPrefab = _instantiator.InstantiatePrefab(Resources.Load(AssetPath.TowerPath));

            TowerFacade towerFacade = towerPrefab.GetComponent<TowerFacade>();
            towerFacade.transform.position = _getTargetPosition.GetPosition();

            _gameLoopService.AddGameListener(towerFacade);
            
            return towerFacade;
        }
    }
}