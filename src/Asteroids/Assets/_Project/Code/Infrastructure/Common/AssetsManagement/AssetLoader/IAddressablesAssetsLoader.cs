using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Infrastructure.Common.AssetsManagement.AssetLoader
{
    public interface IAddressablesAssetsLoader
    {
        UniTask Initialize();
        UniTask<T> LoadAsset<T>(string key) where T : class;
        UniTask LoadAssetsByLabels<T>(params string[] labels) where T : class;
    }
}