using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.Common.AssetsManagement.AssetLoader
{
    public interface IAddressablesAssetsLoader
    {
        Task Initialize();
        Task<T> LoadAsset<T>(string key) where T : class;
        Task<T> LoadAsset<T>(AssetReference reference) where T : class;
        Task<TAsset[]> LoadAll<TAsset>(List<string> keys) where TAsset : class;
        Task<List<string>> GetAssetsListByLabel(string label, Type type);
        Task<List<string>> GetAssetsListByLabel<T>(string label);
        void Release();
    }
}