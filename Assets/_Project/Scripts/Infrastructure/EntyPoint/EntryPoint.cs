using _Project.Scripts.UI;
using _Project.Static;
using DG.Tweening;
using Infrastructure.Services;
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

        public EntryPoint(
            SceneLoader sceneLoader,
            Curtain curtain)
        {
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        void IInitializable.Initialize()
        {
            Debug.unityLogger.logEnabled = Debug.isDebugBuild;
            Application.targetFrameRate = 120;
            DOTween.Init();

            _ = _sceneLoader.LoadScene(Scenes.Gameplay, _curtain.Hide);
        }
    }
}