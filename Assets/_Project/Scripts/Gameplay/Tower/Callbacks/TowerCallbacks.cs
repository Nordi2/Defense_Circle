using _Project.Scripts.Gameplay.Tower.Animation;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Tower.Callbacks
{
    [UsedImplicitly]
    public class TowerCallbacks
    {
        private readonly AnimationTower _animationTower;

        public TowerCallbacks(AnimationTower animationTower)
        {
            _animationTower = animationTower;
        }

        public void GameStart(GameObject towerGameObject) => 
            _animationTower.PlayInitialSpawn(() => towerGameObject.SetActive(true));

        public void TakeDamageCallback(int damage)
        {
            _animationTower.PlayTakeDamage();
        }

        public void DeathCallback()
        {
            _animationTower.PlayDeath();
        }
    }
}