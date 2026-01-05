using System;
using Cysharp.Threading.Tasks;

public interface ISceneLoader
{
    UniTask LoadScene(GameScenes scene);
    void LoadAnyScene(GameScenes scene, Action onLoaded = null);
}