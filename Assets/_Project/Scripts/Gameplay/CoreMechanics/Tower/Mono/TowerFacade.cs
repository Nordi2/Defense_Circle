using _Project.Cor.Component;
using _Project.Cor.Interfaces;
using _Project.Meta.StatsLogic.Upgrade;
using DebugToolsPlus;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace _Project.Cor.Tower.Mono
{
    public class TowerFacade : MonoBehaviour,
        IGameStartListener,
        ITakeDamagble
    {
        private TowerCallbacks _towerCallbacks;
        private TakeDamageComponent _takeDamageComponent;
        private ShowStatsService _showStatsService;
        
        [Inject]
        private void Construct(
            TakeDamageComponent takeDamageComponent,
            TowerCallbacks towerCallbacks,
            ShowStatsService showStatsService)
        {
            _towerCallbacks = towerCallbacks;
            _takeDamageComponent = takeDamageComponent;
            _showStatsService = showStatsService;
        }

        public void OnGameStart()
        {
            _towerCallbacks.GameStart(gameObject);

            D.Log("Tower Stats:", D.FormatText(_showStatsService.ToString(), DColor.RED), DColor.YELLOW);
        }

        public void TakeDamage(int damage)
        {
            _takeDamageComponent.TakeDamage(
                damage,
                out bool isDie,
                _towerCallbacks.TakeDamageCallback,
                GetType(),
                gameObject);

            if (isDie)
                _towerCallbacks.DeathCallback();
        }
    }
}