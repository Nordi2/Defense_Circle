﻿using _Project.Cor.Tower.Animation;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Cor.Tower
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
            _animationTower.PlayTakeDamage(damage);
        }

        public void DeathCallback() =>
            _animationTower.PlayDeath();
    }
}