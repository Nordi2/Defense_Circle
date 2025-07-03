using _Project.Infrastructure.AssetManagement;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Infrastructure.Services.AssetManagement
{
    [UsedImplicitly]
    public class AssetProvider :
        IAssetProvider
    {
        public GameObject LoadAsset(string path) => 
            Resources.Load<GameObject>(path);
    }
}