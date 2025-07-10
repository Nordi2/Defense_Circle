using System;
using _Project.Extensions;
using Infrastructure.Signals;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Component
{
    public class TimeScalePresenter :
        IDisposable
    {
        private readonly TimeScaleView _view;
        private readonly SignalBus _signalBus;
        
        private PauseGameSignal _pauseGameSignal;
        private ResumeGameSignal _resumeGameSignal;
        
        public TimeScalePresenter(
            TimeScaleView view,
            SignalBus signalBus)
        {
            _view = view;
            _signalBus = signalBus;
        }

        public void Subscribe()
        {
            _pauseGameSignal = new PauseGameSignal();
            _resumeGameSignal = new ResumeGameSignal();
            
            _view.PauseButton.AddListener(ClickPauseButton);
            _view.OneTimeScaleButton.AddListener(ClickOneTimeScaleButton);
            _view.TwoTimeScaleButton.AddListener(ClickTwoTimeScaleButton);
            _view.ThreeTimeScaleButton.AddListener(ClickThreeTimeScaleButton);
        }

        void IDisposable.Dispose()
        {
            _view.PauseButton.RemoveListener(ClickPauseButton);
            _view.OneTimeScaleButton.RemoveListener(ClickOneTimeScaleButton);
            _view.TwoTimeScaleButton.RemoveListener(ClickTwoTimeScaleButton);
            _view.ThreeTimeScaleButton.RemoveListener(ClickThreeTimeScaleButton);
        }

        private void ClickPauseButton()
        {
            _signalBus.Fire(_pauseGameSignal);
        }

        private void ClickOneTimeScaleButton()
        {
            _signalBus.Fire(_resumeGameSignal);
            Time.timeScale = 1;
        }

        private void ClickTwoTimeScaleButton()
        {
            _signalBus.Fire(_resumeGameSignal);
            Time.timeScale = 2;
        }

        private void ClickThreeTimeScaleButton()
        {
            _signalBus.Fire(_resumeGameSignal);
            Time.timeScale = 3;
        }
    }
}