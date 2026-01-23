using _Project.Code.Infrastructure.Common.SceneLoader;
using Cysharp.Threading.Tasks;

namespace _Project.Code.GameFlow.States.Core
{
    public class LoadSceneState : IStateWithArg<GameScenes>
    {
        private readonly ISceneLoader _sceneLoader;
    
        public LoadSceneState(ISceneLoader sceneLoader) => 
            _sceneLoader = sceneLoader;

        public async UniTask Enter(GameScenes scene) => 
            await _sceneLoader.LoadScene(scene);

        public UniTask Exit() => default;
    }
}