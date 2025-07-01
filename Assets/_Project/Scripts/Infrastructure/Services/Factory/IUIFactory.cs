using _Project.Scripts.UI;
using _Project.Scripts.UI.Shop;

namespace _Project.Infrastructure.Services
{
    public interface IUIFactory
    {
        ShopPresenter CreateShop();
        InitialTextLoadAfterLoading CreateInitialTextLoadAfterLoading();
        MenuPresenter CreateMenu();
    }
}