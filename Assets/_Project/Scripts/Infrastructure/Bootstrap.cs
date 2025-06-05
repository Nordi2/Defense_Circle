using _Project.Scripts.UI;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    [UsedImplicitly]
    public class Bootstrap :
        IInitializable
    {
        private readonly SceneLoader _sceneLoader;
        private readonly Curtain _curtain;
        
        public Bootstrap(
            SceneLoader sceneLoader,
            Curtain curtain)
        {
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        void IInitializable.Initialize()
        {
            Debug.unityLogger.logEnabled = Debug.isDebugBuild;
            Application.targetFrameRate = 60;
            DOTween.Init();
            
            _ = _sceneLoader.LoadScene(Scenes.Gameplay, _curtain.Hide);
        }
    }
}