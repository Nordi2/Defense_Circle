using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Infrastructure
{
    [UsedImplicitly]
    public class SceneLoader
    {
        public async UniTask LoadScene(string sceneName, Action loadedCallback = null)
        {
            if (sceneName == SceneManager.GetActiveScene().name)
            {
                loadedCallback?.Invoke();
                return;
            }

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            await asyncOperation;
            
            loadedCallback?.Invoke();
        }
    }
}