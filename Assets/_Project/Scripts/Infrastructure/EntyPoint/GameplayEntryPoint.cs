using _Project.Cor.Tower.Mono;
using _Project.Infrastructure.Services;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using _Project.Scripts.UI;
using _Project.UI.Shop;
using Infrastructure.Services;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.EntryPoint
{
    [UsedImplicitly]
    public class GameplayEntryPoint :
        IInitializable
    {
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public GameplayEntryPoint(
            IGameFactory gameFactory,
            IUIFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        void IInitializable.Initialize()
        {
            ShopView shopView = _uiFactory.CreateShop();
            InitialTextLoadAfterLoading initialText = _uiFactory.CreateInitialTextLoadAfterLoading();

            TowerFacade tower = _gameFactory.CreateTower();

            tower.gameObject.SetActive(false);
            shopView.gameObject.SetActive(false);
            initialText.StartAnimation();
        }
    }
}