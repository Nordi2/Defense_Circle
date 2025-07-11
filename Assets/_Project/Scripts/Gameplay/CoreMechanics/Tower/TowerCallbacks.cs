using _Project.Cor.Tower.Animation;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Cor.Tower
{
    [UsedImplicitly]
    public class TowerCallbacks
    {
        private const float _multiplier = 0.02f;

        private readonly AnimationTower _animationTower;

        public TowerCallbacks(AnimationTower animationTower)
        {
            _animationTower = animationTower;
        }

        public void GameStart(GameObject towerGameObject) =>
            _animationTower.PlayInitialSpawn(() => towerGameObject.SetActive(true));

        public void TakeDamageCallback(int damage)
        {
            float amountStrength = damage * _multiplier;
            _animationTower.PlayTakeDamage(amountStrength);
        }

        public void DeathCallback() =>
            _animationTower.PlayDeath();
    }
}