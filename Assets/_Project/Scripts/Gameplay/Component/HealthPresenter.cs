using System;
using JetBrains.Annotations;
using R3;
using Zenject;

namespace _Project.Scripts.Gameplay.Component
{
    [UsedImplicitly]
    public class HealthPresenter :
        IInitializable,
        IDisposable
    {
        private readonly HealthComponent _healthComponent;
        private readonly HealthView _view;

        private readonly CompositeDisposable _disposable = new();

        public HealthPresenter(
            HealthComponent healthComponent,
            HealthView view)
        {
            _healthComponent = healthComponent;
            _view = view;
        }

        void IInitializable.Initialize()
        {
            _healthComponent
                .OnHealthChanged
                .Prepend((_healthComponent.CurrentHealth,_healthComponent.CurrentHealth))
                .Subscribe(UpdateCurrentHealthText)
                .AddTo(_disposable);
        }


        private void UpdateMaxHealthText(int newMaxValue) =>
            _view.UpdateMaxHealthText(newMaxValue);

        private void UpdateCurrentHealthText((int oldValue, int currentValue) newValue)
        {
            _view.UpdateCurrentHealthText(newValue.oldValue, newValue.currentValue);
        }

        void IDisposable.Dispose()
        {
            _disposable.Dispose();
        }
    }
}