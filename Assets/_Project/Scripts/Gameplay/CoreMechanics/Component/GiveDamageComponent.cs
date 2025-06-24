using System;
using _Project.Cor.Interfaces;
using _Project.Meta.StatsLogic.NoneUpgrade;
using JetBrains.Annotations;

namespace _Project.Cor.Component
{
    [UsedImplicitly]
    public class GiveDamageComponent
    {
        private readonly CollisionDamageStats _collisionDamageStatsShow;
        
        public GiveDamageComponent(CollisionDamageStats collisionDamageStatsShow)
        {
            _collisionDamageStatsShow = collisionDamageStatsShow;
        }
        
        public void GiveDamage(ITakeDamagble takeDamageObject,Action giveDamageCallback = null)
        {
            takeDamageObject.TakeDamage(_collisionDamageStatsShow.Damage);
            giveDamageCallback?.Invoke();
        }
    }
}