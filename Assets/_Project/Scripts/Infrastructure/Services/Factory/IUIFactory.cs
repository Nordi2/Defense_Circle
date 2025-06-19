using _Project.UI.Shop;

namespace _Project.Infrastructure.Services
{
    public interface IUIFactory
    {
        ShopView CreateShop();
        InitialTextLoadAfterLoading CreateInitialTextLoadAfterLoading();
    }
}