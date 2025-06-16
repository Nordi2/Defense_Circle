using System;
using Cor.Interfaces;
using JetBrains.Annotations;
using Meta.Stats.NoneUpgrade;

namespace Cor.Component
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