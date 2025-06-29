using System;
using _Project.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI
{
    [UsedImplicitly]
    public class MenuPresenter :
        IInitializable,
        IDisposable
    {
        public event Action OnStartNextWave;
        public event Action OnOpenShop;

        private readonly MenuView _view;

        public MenuPresenter(MenuView view)
        {
            _view = view;
        }

        void IInitializable.Initialize()
        {
            _view.ContinueButton.AddListener(StartNextWave);
            _view.ShopButton.AddListener(OpenShop);
        }

        void IDisposable.Dispose()
        {
            _view.ContinueButton.RemoveListener(StartNextWave);
            _view.ShopButton.RemoveListener(OpenShop);
        }

        public void OpenMenu() => 
            _view.Show();

        private void OpenShop() =>
            OnOpenShop?.Invoke();

        private void StartNextWave() =>
            OnStartNextWave?.Invoke();
    }
}