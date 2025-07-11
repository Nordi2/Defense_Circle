using _Project.Cor.Spawner;
using _Project.Infrastructure.Services.AssetManagement;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Shop;
using _Project.UI.Shop;
using Infrastructure.Services;
using JetBrains.Annotations;
using R3;
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
        private readonly CompositeDisposable _disposable;
        private readonly IGameFactory _gameFactory;
        
        public ShopPresenter ShopPresenter { get; private set; }
        
        public UIFactory(
            UIRoot uiRoot,
            Wallet wallet,
            StatsStorage statsStorage,
            GameLoopService gameLoopService,
            CompositeDisposable disposable,
            IGameFactory gameFactory)
        {
            _uiRoot = uiRoot;
            _wallet = wallet;
            _statsStorage = statsStorage;
            _gameLoopService = gameLoopService;
            _disposable = disposable;
            _gameFactory = gameFactory;
        }
        
        public ShopPresenter CreateShop(WaveSpawner spawner)
        {
            GameObject shopPrefab =
                Object.Instantiate(Resources.Load<GameObject>(AssetPath.ShopUpgradePath));

            ShopView view = shopPrefab.GetComponent<ShopView>();
            ShopPresenter shopPresenter = new ShopPresenter(_wallet, view, _statsStorage,_gameFactory.WavePresenter);
            ShopController shopController = new ShopController(shopPresenter,spawner,_disposable);
            
            _uiRoot.AddToContainer(view.GetComponent<RectTransform>());
            shopPresenter.CreateUpgradeCarts();

            _gameLoopService.AddDisposable(shopPresenter,shopController);
            _gameLoopService.AddInitializable(shopController);

            view.gameObject.SetActive(false);
            
            ShopPresenter = shopPresenter;

            return shopPresenter;
        }

        public InitialTextLoadAfterLoading CreateInitialTextLoadAfterLoading()
        {
            GameObject textPrefab = Object.Instantiate(Resources.Load<GameObject>(AssetPath.InitialTextLoad));

            InitialTextLoadAfterLoading view = textPrefab.GetComponent<InitialTextLoadAfterLoading>();

            _uiRoot.AddToContainer(view.GetComponent<RectTransform>());
            _gameLoopService.AddGameListener(view);
        
            return view;
        }
    }
}