using UnityEngine;

namespace Code.Logic.Gameplay.Services.GameFactory
{
    public interface IAssetsLoader
    {
        T Load<T>(string path) where T : MonoBehaviour;
    }
}