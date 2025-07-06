using _Project.Cor.Spawner;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Shop;

namespace _Project.Infrastructure.Services
{
    public interface IUIFactory
    {
        ShopPresenter CreateShop(IEndWaveEvent spawner);
        InitialTextLoadAfterLoading CreateInitialTextLoadAfterLoading();
        MenuPresenter CreateMenu();
    }
}