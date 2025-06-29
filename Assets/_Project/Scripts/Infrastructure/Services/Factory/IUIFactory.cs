using System;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Shop;
using _Project.UI.Shop;

namespace _Project.Infrastructure.Services
{
    public interface IUIFactory
    {
        (ShopPresenter,ShopView) CreateShop(MenuPresenter menuPresenter);
        InitialTextLoadAfterLoading CreateInitialTextLoadAfterLoading();
        MenuPresenter CreateMenu();
    }
}