using UnityEngine;

namespace Code.Logic.Gameplay.Services.Factories.GameFactory
{
    public interface IAssetsLoader
    {
        T Load<T>(string path) where T : MonoBehaviour;
    }
}