using System;
using System.Collections.Generic;
using Cor.Enemy.Mono;
using Cor.Observers;
using Cor.Enemy;
using JetBrains.Annotations;
using R3;
using Zenject;

namespace Cor.Tower
{
    [UsedImplicitly]
    public class EnemysVault : 
        IInitializable,
        IDisposable
    {
        private readonly EnemyObserver _enemyObserver;

        private readonly List<EnemyFacade> _enemies = new();
        private readonly CompositeDisposable _disposable;

        public EnemysVault(
            EnemyObserver enemyObserver,
            CompositeDisposable disposable)
        {
            _enemyObserver = enemyObserver;
            _disposable = disposable;
        }

        public IReadOnlyList<EnemyFacade> Enemies => _enemies;

        void IInitializable.Initialize()
        {
            _enemyObserver
                .TriggerEnter
                .Subscribe(AddEnemy)
                .AddTo(_disposable);
        }

        void IDisposable.Dispose() => 
            _disposable.Dispose();

        private void AddEnemy(EnemyFacade enemyFacade)
        {
            enemyFacade.OnDeath += DeleteEnemyFromList;
            _enemies.Add(enemyFacade);
        }

        private void DeleteEnemyFromList(EnemyFacade concreteEnemyFacade)
        {
            if (_enemies.Contains(concreteEnemyFacade))
            {
                _enemies.Remove(concreteEnemyFacade);
                concreteEnemyFacade.OnDeath -= DeleteEnemyFromList;
            }
        }
    }
}