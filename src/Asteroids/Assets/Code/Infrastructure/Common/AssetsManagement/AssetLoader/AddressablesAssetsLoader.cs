using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Code.Infrastructure.Common.AssetsManagement.AssetLoader
{
    public sealed class AddressablesAssetsLoader : IAddressablesAssetsLoader
    {
        private readonly Dictionary<string, AsyncOperationHandle> _handles = new();

        public async UniTask Initialize() => await Addressables.InitializeAsync().Task;

        public async UniTask<T> LoadAsset<T>(string key) where T : class
        {
            if (_handles.TryGetValue(key, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
        
            handle.Completed += h => _handles[key] = h;
        
            return await handle.Task;
        }
    }
}