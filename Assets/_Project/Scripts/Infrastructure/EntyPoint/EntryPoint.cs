using _Project.Scripts.UI;
using _Project.Static;
using DG.Tweening;
using Infrastructure.Services;
using Infrastructure.Services.Services.LoadData;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.EntryPoint
{
    [UsedImplicitly]
    public class EntryPoint :
        IInitializable
    {
        private readonly SceneLoader _sceneLoader;
        private readonly Curtain _curtain;
        private readonly IGameLoadDataService _gameLoadDataService;
        
        public EntryPoint(
            SceneLoader sceneLoader,
            Curtain curtain,
            IGameLoadDataService gameLoadDataService)
        {
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameLoadDataService = gameLoadDataService;
        }

        void IInitializable.Initialize()
        {
            Debug.unityLogger.logEnabled = Debug.isDebugBuild;
            Application.targetFrameRate = 120;
            DOTween.Init();
            _gameLoadDataService.LoadData();
            
            _ = _sceneLoader.LoadScene(Scenes.Gameplay, _curtain.Hide);
        }
    }
}