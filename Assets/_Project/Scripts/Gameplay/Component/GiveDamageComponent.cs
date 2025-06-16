using System;
using _Project.Scripts.Gameplay.Stats.EnemyStats;
using _Project.Scripts.Gameplay.Tower;
using JetBrains.Annotations;

namespace _Project.Scripts.Gameplay.Component
{
    [UsedImplicitly]
    public class GiveDamageComponent
    {
        private readonly CollisionDamageStats _collisionDamage;
        
        public GiveDamageComponent(CollisionDamageStats collisionDamage)
        {
            _collisionDamage = collisionDamage;
        }
        
        public void GiveDamage(ITakeDamagble takeDamageObject,Action giveDamageCallback = null)
        {
            takeDamageObject.TakeDamage(_collisionDamage.Damage);
            giveDamageCallback?.Invoke();
        }
    }
}