using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

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
        
        public async UniTask<TAsset[]> LoadAll<TAsset>(List<string> keys) where TAsset : class => 
            await UniTask.WhenAll(keys.Select(LoadAsset<TAsset>).ToList());

        public async UniTask<List<string>> GetAssetsListByLabel<T>(string label) =>
            await GetAssetsListByLabel(label, typeof(T));
        
        private async UniTask<List<string>> GetAssetsListByLabel(string label, Type type)
        {
            AsyncOperationHandle<IList<IResourceLocation>> handle = 
                Addressables.LoadResourceLocationsAsync(label, type);

            IList<IResourceLocation> locations = await handle.Task;
            
            List<string> assetKeys = locations.Select(location => location.PrimaryKey).ToList();

            Addressables.Release(handle);
        
            return assetKeys;
        }
    }
}