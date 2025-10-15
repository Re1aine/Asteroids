using UnityEngine;

namespace Code.Infrastructure.Common.AssetsManagement.AssetsLoader
{
    public interface IAssetsLoader
    {
        T Load<T>(string path) where T : MonoBehaviour;
    }
}