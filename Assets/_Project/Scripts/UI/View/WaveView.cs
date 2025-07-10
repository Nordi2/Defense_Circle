using System;
using _Project.Data.Config;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Component
{
    public class WaveView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeToNextWave;
        [SerializeField] private TextMeshProUGUI _currentWave;
        [SerializeField] private TextMeshProUGUI _maxWave;
        [SerializeField] private TextMeshProUGUI _titleWave;
        [SerializeField] private Image _progressBar;
        [SerializeField] private AnimationSettingsWaveView _animationSettings;

        private Sequence _sequenceTimeToNextWave;

        public void StartTimerToNextWaveAnimation(Action callback)
        {
            _animationSettings
                .AnimationTimerToNextWave(_timeToNextWave, callback)
                .Restart();
        }

        public void UpdateCurrentNumberWave(string numberWave) => 
            _currentWave.text = numberWave;
        
        public void UpdateMaxNumberWave(string numberWave) =>
            _maxWave.text = numberWave;
    }
}