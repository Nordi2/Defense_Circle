using System;
using _Project.Scripts.Gameplay.EnemyLogic;
using _Project.Scripts.Gameplay.Stats;
using R3;
using Zenject;

namespace _Project.Scripts.Gameplay.Component
{
    public class DeathComponent : 
        IInitializable
    {
        private readonly Action _deathCallback;
        private readonly HealthStat _healthStat;

        private readonly CompositeDisposable _disposable = new();
        
        public DeathComponent(
            HealthStat healthStat,
            IDieble dieble)
        {
            _healthStat = healthStat;
            _deathCallback = dieble.Die;
        }

        void IInitializable.Initialize()
        {
            _healthStat
                .CurrentHealth
                .Where(value=> value <= 0)
                .Subscribe(_  => _deathCallback?.Invoke())
                .AddTo(_disposable);
        }
    }
}