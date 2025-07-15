using System;
using _Project.Cor.Interfaces;
using _Project.Meta.StatsLogic.NoneUpgrade;
using JetBrains.Annotations;

namespace _Project.Cor.Component
{
    [UsedImplicitly]
    public class GiveDamageComponent
    {
        private readonly CollisionDamageStat _collisionDamageStatShow;
        
        public GiveDamageComponent(CollisionDamageStat collisionDamageStatShow)
        {
            _collisionDamageStatShow = collisionDamageStatShow;
        }
        
        public void GiveDamage(ITakeDamagble takeDamageObject,Action giveDamageCallback = null)
        {
            takeDamageObject.TakeDamage(_collisionDamageStatShow.Damage);
            giveDamageCallback?.Invoke();
        }
    }
}