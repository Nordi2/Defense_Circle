using UnityEngine;

namespace _Project.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        GameObject LoadAsset(string path);
    }
}