using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.Common.AssetsManagement.AssetLoader
{
    public interface IAddressablesAssetsLoader
    {
        UniTask Initialize();
        UniTask<T> LoadAsset<T>(string key) where T : class;
    }
}