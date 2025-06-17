using System;
using _Project.Cor.Interfaces;
using _Project.Meta.Stats.NoneUpgrade;
using JetBrains.Annotations;

namespace _Project.Cor.Component
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