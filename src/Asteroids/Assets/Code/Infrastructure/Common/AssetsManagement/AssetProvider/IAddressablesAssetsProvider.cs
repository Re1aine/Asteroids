using System.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.Common.AssetsManagement.AssetProvider
{
    public interface IAddressablesAssetsProvider
    {
        Task<T> Instantiate<T>(string assetPath) where T : class; 
        Task<T> InstantiateAt<T>(string assetPath, Vector3 position, Quaternion rotation) where T : class;
        Task<T> Instantiate<T>(string assetPath, Transform parent) where T : class;
    }
}