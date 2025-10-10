using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Logic.Gameplay.Services.Factories.GameFactory
{
    public class AssetsProvider : IAssetsProvider
    {
        private readonly IObjectResolver _objectResolver;
        private readonly IAssetsLoader _assetsLoader;

        public AssetsProvider(IObjectResolver objectResolver, IAssetsLoader assetsLoader)
        {
            _objectResolver = objectResolver;
            _assetsLoader = assetsLoader;
        }

        public T Instantiate<T>(string assetPath) where T : MonoBehaviour
        {
            T prefab = _assetsLoader.Load<T>(assetPath);
            return _objectResolver.Instantiate(prefab);
        }

        public T InstantiateAt<T>(string assetPath, Vector3 position, Quaternion rotation) where T : MonoBehaviour
        {
            T prefab = _assetsLoader.Load<T>(assetPath);
            return _objectResolver.Instantiate(prefab, position, rotation);
        }

        public T Instantiate<T>(string assetPath, Transform parent) where T : MonoBehaviour
        {
            T prefab = _assetsLoader.Load<T>(assetPath);
            return _objectResolver.Instantiate(prefab, parent);
        }
    }
}