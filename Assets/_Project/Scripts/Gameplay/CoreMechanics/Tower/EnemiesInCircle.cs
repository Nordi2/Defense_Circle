using System;
using System.Collections.Generic;
using _Project.Cor.Enemy.Mono;
using _Project.Cor.Observers;
using JetBrains.Annotations;
using R3;
using Zenject;

namespace _Project.Cor.Tower
{
    [UsedImplicitly]
    public class EnemiesInCircle : 
        IInitializable,
        IDisposable
    {
        private readonly EnemyObserver _enemyObserver;

        private readonly List<EnemyFacade> _enemies = new();
        private readonly CompositeDisposable _disposable;

        public EnemiesInCircle(
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
            if (!_enemies.Contains(concreteEnemyFacade)) 
                return;
            
            _enemies.Remove(concreteEnemyFacade);
            concreteEnemyFacade.OnDeath -= DeleteEnemyFromList;
        }
    }
}