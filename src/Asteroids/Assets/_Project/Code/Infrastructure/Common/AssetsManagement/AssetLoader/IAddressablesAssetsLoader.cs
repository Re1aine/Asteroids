using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.Common.AssetsManagement.AssetLoader
{
    public interface IAddressablesAssetsLoader
    {
        UniTask Initialize();
        UniTask<T> LoadAsset<T>(string key) where T : class;
        UniTask<TAsset[]> LoadAll<TAsset>(List<string> keys) where TAsset : class;
        UniTask<List<string>> GetAssetsListByLabel<T>(string label);
    }
}