using System.Collections.Generic;
using System.Threading.Tasks;

namespace Code.Infrastructure.Common.AssetsManagement.AssetLoader
{
    public interface IAddressablesAssetsLoader
    {
        Task Initialize();
        Task<T> LoadAsset<T>(string key) where T : class;
        Task<TAsset[]> LoadAll<TAsset>(List<string> keys) where TAsset : class;
        Task<List<string>> GetAssetsListByLabel<T>(string label);
    }
}