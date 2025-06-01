using System;
using _Project.Scripts.Gameplay.Stats;
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
        private readonly HealthView _view;
        private readonly HealthStat _healthStat;
        
        private readonly CompositeDisposable _disposable = new();

        public HealthPresenter(
            HealthView view,
            HealthStat healthStat)
        {
            _view = view;
            _healthStat = healthStat;
        }

        void IInitializable.Initialize()
        {
            _healthStat
                .CurrentHealth
                .Prepend(_healthStat.MaxHealth.CurrentValue)
                .Pairwise()
                .Subscribe(pair => UpdateCurrentHealthText(pair.Previous,pair.Current))
                .AddTo(_disposable);
        }
        
        private void UpdateMaxHealthText(int newMaxValue) =>
            _view.UpdateMaxHealthText(newMaxValue);

        private void UpdateCurrentHealthText(int oldValue, int newValue)
        {
            _view.UpdateCurrentHealthText(oldValue, newValue);      
        }

        void IDisposable.Dispose()
        {
            _disposable.Dispose();
        }
    }
}