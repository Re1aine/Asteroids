using System;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Infrastructure.Common.SceneLoader
{
    public interface ISceneLoader
    {
        UniTask LoadScene(GameScenes scene);
        void LoadAnyScene(GameScenes scene, Action onLoaded = null);
    }
}