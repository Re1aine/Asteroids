using Code.Infrastructure.Common.AssetsManagement.AssetLoader;
using Code.Infrastructure.Common.SceneLoader;
using Code.Logic.Services.SDKInitializer;
using Cysharp.Threading.Tasks;

namespace Code.GameFlow.States.Core
{
    public class ProjectInitState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAddressablesAssetsLoader _addressablesAssetsLoader;
        private readonly ISDKInitializer _sdkInitializer;

        public ProjectInitState(GameStateMachine gameStateMachine,
            IAddressablesAssetsLoader addressablesAssetsLoader,
            ISDKInitializer sdkInitializer)
        {
            _gameStateMachine = gameStateMachine;
            _addressablesAssetsLoader = addressablesAssetsLoader;
            _sdkInitializer = sdkInitializer;
        }

        public async UniTask Enter()
        {
            await _addressablesAssetsLoader.Initialize();
            await _sdkInitializer.Initialize();
            
            _gameStateMachine
                .Enter<LoadSceneState, GameScenes>(GameScenes.Menu)
                .Forget();
        }

        public UniTask Exit() => default;
    }
}