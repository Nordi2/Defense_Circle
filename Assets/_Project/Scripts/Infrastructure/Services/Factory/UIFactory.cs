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
        
        public UIFactory(
            UIRoot uiRoot,
            CompositeDisposable disposable,
            Wallet wallet,
            StatsStorage statsStorage,
            GameLoopService gameLoopService)
        {
            _uiRoot = uiRoot;
            _disposable = disposable;
            _wallet = wallet;
            _statsStorage = statsStorage;
            _gameLoopService = gameLoopService;
        }

        public (ShopPresenter, ShopView) CreateShop()
        {
            D.Log(GetType().Name ,"Create SHOP",DColor.GREEN,true);
            
            GameObject shopPrefab =
                Object.Instantiate(Resources.Load<GameObject>(AssetPath.ShopUpgradePath));

            ShopView view = shopPrefab.GetComponent<ShopView>();
            ShopPresenter shopPresenter = new ShopPresenter(_wallet, view, _disposable, _statsStorage);

            _uiRoot.AddToContainer(view.GetComponent<RectTransform>());
            shopPresenter.CreateUpgradeCarts();

            return (shopPresenter, view);
        }

        public InitialTextLoadAfterLoading CreateInitialTextLoadAfterLoading()
        {
            D.Log(GetType().Name ,"Create INITIAL-TEXT",DColor.GREEN,true);
            
            GameObject textPrefab = Object.Instantiate(Resources.Load<GameObject>(AssetPath.InitialTextLoad));

            InitialTextLoadAfterLoading view = textPrefab.GetComponent<InitialTextLoadAfterLoading>();
            
            _uiRoot.AddToContainer(view.GetComponent<RectTransform>());
            _gameLoopService.AddGameListener(view);
            
            return view;
        }
    }
}