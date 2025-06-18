using System;
using _Project.Cor.Interfaces;
using _Project.Meta.StatsLogic.NoneUpgrade;
using JetBrains.Annotations;

namespace _Project.Cor.Component
{
    [UsedImplicitly]
    public class GiveDamageComponent
    {
        private readonly CollisionDamageShowStatsInfo _collisionDamageShow;
        
        public GiveDamageComponent(CollisionDamageShowStatsInfo collisionDamageShow)
        {
            _collisionDamageShow = collisionDamageShow;
        }
        
        public void GiveDamage(ITakeDamagble takeDamageObject,Action giveDamageCallback = null)
        {
            takeDamageObject.TakeDamage(_collisionDamageShow.Damage);
            giveDamageCallback?.Invoke();
        }
    }
}