using _Project.Data.Config;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Shop;
using _Project.Static;
using _Project.UI.Shop;
using DebugToolsPlus;
using Infrastructure.Services;
using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace _Project.Infrastructure.Services
{
    [UsedImplicitly]
    public class UIFactory :
        IUIFactory
    {
        private readonly UIRoot _uiRoot;
        private readonly StatsStorage _statsStorage;
        private readonly Wallet _wallet;
        private readonly CompositeDisposable _disposable;
        private readonly GameLoopService _gameLoopService;
        private readonly GameConfig _gameConfig;
        
        public UIFactory(
            UIRoot uiRoot,
            CompositeDisposable disposable,
            Wallet wallet,
            StatsStorage statsStorage,
            GameLoopService gameLoopService, GameConfig gameConfig)
        {
            _uiRoot = uiRoot;
            _disposable = disposable;
            _wallet = wallet;
            _statsStorage = statsStorage;
            _gameLoopService = gameLoopService;
            _gameConfig = gameConfig;
        }

        public (ShopPresenter, ShopView) CreateShop()
        {
            D.Log(GetType().Name, "Create SHOP", DColor.GREEN, true);

            GameObject shopPrefab =
                Object.Instantiate(Resources.Load<GameObject>(AssetPath.ShopUpgradePath));

            ShopView view = shopPrefab.GetComponent<ShopView>();
            ShopPresenter shopPresenter = new ShopPresenter(_wallet, view, _statsStorage,_gameConfig.AmountUpgradeCart);

            _uiRoot.AddToContainer(view.GetComponent<RectTransform>());
            shopPresenter.CreateUpgradeCarts();

            _gameLoopService.AddDisposable(shopPresenter);

            return (shopPresenter, view);
        }

        public InitialTextLoadAfterLoading CreateInitialTextLoadAfterLoading()
        {
            D.Log(GetType().Name, "Create INITIAL-TEXT", DColor.GREEN, true);

            GameObject textPrefab = Object.Instantiate(Resources.Load<GameObject>(AssetPath.InitialTextLoad));

            InitialTextLoadAfterLoading view = textPrefab.GetComponent<InitialTextLoadAfterLoading>();

            _uiRoot.AddToContainer(view.GetComponent<RectTransform>());
            _gameLoopService.AddGameListener(view);

            return view;
        }
    }
}