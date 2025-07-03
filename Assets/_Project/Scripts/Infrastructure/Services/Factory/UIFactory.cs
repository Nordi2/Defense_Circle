using _Project.Infrastructure.Services.AssetManagement;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Shop;
using _Project.UI.Shop;
using DebugToolsPlus;
using Infrastructure.Services;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Infrastructure.Services
{
    [UsedImplicitly]
    public class UIFactory :
        IUIFactory
    {
        private readonly UIRoot _uiRoot;
        private readonly StatsStorage _statsStorage;
        private readonly Wallet _wallet;
        private readonly GameLoopService _gameLoopService;
        
        public MenuPresenter MenuPresenter { get; private set; }
        public ShopPresenter ShopPresenter { get; private set; }
        
        public UIFactory(
            UIRoot uiRoot,
            Wallet wallet,
            StatsStorage statsStorage,
            GameLoopService gameLoopService)
        {
            _uiRoot = uiRoot;
            _wallet = wallet;
            _statsStorage = statsStorage;
            _gameLoopService = gameLoopService;
        }

        public MenuPresenter CreateMenu()
        {
            D.Log(GetType().Name, "Create Menu", DColor.GREEN, true);

            GameObject menuPrefab = Object.Instantiate(Resources.Load<GameObject>(AssetPath.MenuPath));

            MenuView view = menuPrefab.GetComponent<MenuView>();
            MenuPresenter presenter = new MenuPresenter(view);

            _uiRoot.AddToContainer(view.GetComponent<RectTransform>());
            
            view.gameObject.SetActive(false);
            
            _gameLoopService.AddInitializable(presenter);
            _gameLoopService.AddDisposable(presenter);

            MenuPresenter = presenter;
            
            return presenter;
        }

        public ShopPresenter CreateShop()
        {
            D.Log(GetType().Name, "Create SHOP", DColor.GREEN, true);

            GameObject shopPrefab =
                Object.Instantiate(Resources.Load<GameObject>(AssetPath.ShopUpgradePath));

            ShopView view = shopPrefab.GetComponent<ShopView>();
            ShopPresenter shopPresenter = new ShopPresenter(_wallet, view, _statsStorage, MenuPresenter);

            _uiRoot.AddToContainer(view.GetComponent<RectTransform>());
            shopPresenter.CreateUpgradeCarts();

            _gameLoopService.AddDisposable(shopPresenter);
            _gameLoopService.AddInitializable(shopPresenter);

            view.gameObject.SetActive(false);
            
            ShopPresenter = shopPresenter;

            return shopPresenter;
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