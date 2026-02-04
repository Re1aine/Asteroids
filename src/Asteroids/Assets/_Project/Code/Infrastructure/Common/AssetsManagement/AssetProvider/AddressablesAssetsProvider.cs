using _Project.Code.Infrastructure.Common.AssetsManagement.AssetLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Infrastructure.Common.AssetsManagement.AssetProvider
{
    public class AddressablesAssetsProvider : IAddressablesAssetsProvider
    {
        private readonly IObjectResolver _objectResolver;
        private readonly IAddressablesAssetsLoader _assetsLoader;
        
        public AddressablesAssetsProvider(IObjectResolver objectResolver, IAddressablesAssetsLoader assetsLoader)
        {
            _objectResolver = objectResolver;
            _assetsLoader = assetsLoader;
        }

        public async UniTask<T> Instantiate<T>(string assetPath) where T : class
        {
            GameObject prefab = await _assetsLoader.LoadAsset<GameObject>(assetPath);
            return _objectResolver.Instantiate(prefab)
                .GetComponent<T>();
        }

        public async UniTask<T> InstantiateAt<T>(string assetPath, Vector3 position, Quaternion rotation) where T : class
        {
            GameObject prefab = await _assetsLoader.LoadAsset<GameObject>(assetPath);
            return _objectResolver.Instantiate(prefab, position, rotation)
                .GetComponent<T>();
        }

        public async UniTask<T> Instantiate<T>(string assetPath, Transform parent) where T : class
        {
            GameObject prefab = await _assetsLoader.LoadAsset<GameObject>(assetPath);
            return _objectResolver.Instantiate(prefab, parent)
                .GetComponent<T>();
        }
    }
}