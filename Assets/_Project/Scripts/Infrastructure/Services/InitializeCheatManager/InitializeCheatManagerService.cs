#if UNITY_EDITOR

using _Project;
using _Project.Cor.Tower.Mono;
using _Project.Meta.Money;
using _Project.Meta.StatsLogic;
using _Project.Scripts.UI.Shop;
using JetBrains.Annotations;
using Zenject;

namespace Infrastructure.Services.Services.InitializeCheatManager
{
    [UsedImplicitly]
    public class InitializeCheatManagerService
    {
        private readonly DiContainer _container;

        public InitializeCheatManagerService(DiContainer container)
        {
            _container = container;
        }

        public void Init(TowerFacade tower, ShopPresenter presenter)
        {
            CheatManager.TowerFacade = tower;

            CheatManager.ShopPresenter = presenter;
            CheatManager.EnemyPool = _container.Resolve<EnemyPool>();
            CheatManager.StatsStorage = _container.Resolve<StatsStorage>();
            CheatManager.Wallet = _container.Resolve<Wallet>();
        }
    }
}
#endif