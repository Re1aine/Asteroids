using UnityEngine;

public interface IAssetsLoader
{
    T Load<T>(string path) where T : MonoBehaviour;
}