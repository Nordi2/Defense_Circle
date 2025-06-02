using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.EnemyLogic;
using _Project.Scripts.Gameplay.Observers;
using DebugToolsPlus;
using JetBrains.Annotations;
using R3;
using Zenject;

namespace _Project.Scripts.Gameplay.TowerLogic
{
    [UsedImplicitly]
    public class EnemysVault :
        IInitializable,
        IDisposable
    {
        private readonly EnemyObserver _enemyObserver;

        private readonly List<Enemy> _enemies = new();
        private readonly CompositeDisposable _disposable = new();

        public EnemysVault(EnemyObserver enemyObserver)
        {
            _enemyObserver = enemyObserver;
        }

        public IReadOnlyList<Enemy> Enemies => _enemies;

        void IInitializable.Initialize()
        {
            _enemyObserver
                .TriggerEnter
                .Subscribe(AddEnemy)
                .AddTo(_disposable);
        }

        void IDisposable.Dispose()
        {
            _disposable.Dispose();
        }

        private void AddEnemy(Enemy enemy)
        {
            D.Log(GetType().Name, "Add Enemy", DColor.YELLOW, true);

            enemy.OnDeath += DeleteEnemyFromList;
            _enemies.Add(enemy);
        }

        private void DeleteEnemyFromList(Enemy concreteEnemy)
        {
            if (_enemies.Contains(concreteEnemy))
            {
                _enemies.Remove(concreteEnemy);
                concreteEnemy.OnDeath -= DeleteEnemyFromList;
            }
        }
    }
}