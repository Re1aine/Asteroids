using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Code.Infrastructure.Common.AssetsManagement.AssetLoader
{
    public sealed class AddressablesAssetsLoader : IAddressablesAssetsLoader
    {
        private readonly Dictionary<string, AsyncOperationHandle> _handles = new();

        public async Task Initialize() => await Addressables.InitializeAsync().Task;

        public async Task<T> LoadAsset<T>(string key) where T : class
        {
            if (_handles.TryGetValue(key, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
        
            handle.Completed += h => _handles[key] = h;
        
            return await handle.Task;
        }
    
        public async Task<T> LoadAsset<T>(AssetReference reference) where T : class => 
            await LoadAsset<T>(reference.AssetGUID);

        public async Task<TAsset[]> LoadAll<TAsset>(List<string> keys) where TAsset : class => 
            await Task.WhenAll(keys.Select(LoadAsset<TAsset>).ToList());

        public async Task<List<string>> GetAssetsListByLabel<T>(string label) =>
            await GetAssetsListByLabel(label, typeof(T));
        
        public async Task<List<string>> GetAssetsListByLabel(string label, Type type)
        {
            AsyncOperationHandle<IList<IResourceLocation>> handle = 
                Addressables.LoadResourceLocationsAsync(label, type);

            IList<IResourceLocation> locations = await handle.Task;
            
            List<string> assetKeys = locations.Select(location => location.PrimaryKey).ToList();

            Addressables.Release(handle);
        
            return assetKeys;
        }
    
        public void Release()
        {
            foreach (AsyncOperationHandle handle in _handles.Values) 
                Addressables.Release(handle);
            
            _handles.Clear();
        }
    }
}