using System;
using _Project.Meta.StatsLogic.Upgrade;
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
        private readonly HealthStats _healthStats;

        private readonly CompositeDisposable _disposable;

        public HealthPresenter(
            HealthView view,
            HealthStats healthStats,
            CompositeDisposable disposable)
        {
            _view = view;
            _healthStats = healthStats;
            _disposable = disposable;
        }

        void IInitializable.Initialize()
        {
            _healthStats
                .CurrentHealth
                .Prepend(_healthStats.MaxHealth.CurrentValue)
                .Pairwise()
                .Subscribe(pair => UpdateCurrentHealthText(pair.Previous, pair.Current))
                .AddTo(_disposable);
        }

        void IDisposable.Dispose() =>
            _disposable.Dispose();
        
        private void UpdateCurrentHealthText(int oldValue, int newValue) => 
            _view.UpdateCurrentHealthText(oldValue, newValue);
    }
}