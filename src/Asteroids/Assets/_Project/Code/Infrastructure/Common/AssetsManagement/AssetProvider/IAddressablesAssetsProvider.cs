using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.Common.AssetsManagement.AssetProvider
{
    public interface IAddressablesAssetsProvider
    {
        UniTask<T> Instantiate<T>(string assetPath) where T : class; 
        UniTask<T> InstantiateAt<T>(string assetPath, Vector3 position, Quaternion rotation) where T : class;
        UniTask<T> Instantiate<T>(string assetPath, Transform parent) where T : class;
    }
}