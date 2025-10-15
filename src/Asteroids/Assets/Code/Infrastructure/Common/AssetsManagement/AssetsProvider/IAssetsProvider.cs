using UnityEngine;

namespace Code.Infrastructure.Common.AssetsManagement.AssetsProvider
{
    public interface IAssetsProvider
    {
        T Instantiate<T>(string assetPath) where T : MonoBehaviour;
        T InstantiateAt<T>(string assetPath, Vector3 position, Quaternion rotation) where T : MonoBehaviour;
        T Instantiate<T>(string assetPath, Transform parent) where T : MonoBehaviour;
    }
}