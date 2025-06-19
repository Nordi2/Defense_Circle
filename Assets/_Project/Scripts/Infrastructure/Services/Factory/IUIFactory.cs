using _Project.Scripts.UI.Shop;
using _Project.UI.Shop;

namespace _Project.Infrastructure.Services
{
    public interface IUIFactory
    {
        (ShopPresenter,ShopView) CreateShop();
        InitialTextLoadAfterLoading CreateInitialTextLoadAfterLoading();
    }
}