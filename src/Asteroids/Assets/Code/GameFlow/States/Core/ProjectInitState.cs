using Code.Infrastructure.Common.AssetsManagement.AssetLoader;
using Code.Infrastructure.Common.SceneLoader;
using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Services.SDKInitializer;
using Cysharp.Threading.Tasks;

namespace Code.GameFlow.States.Core
{
    public class ProjectInitState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAddressablesAssetsLoader _addressablesAssetsLoader;
        private readonly ISDKInitializer _sdkInitializer;
        private readonly IRepositoriesHolder _repositoriesHolder;

        public ProjectInitState(GameStateMachine gameStateMachine,
            IAddressablesAssetsLoader addressablesAssetsLoader,
            ISDKInitializer sdkInitializer,
            IRepositoriesHolder repositoriesHolder)
        {
            _gameStateMachine = gameStateMachine;
            _addressablesAssetsLoader = addressablesAssetsLoader;
            _sdkInitializer = sdkInitializer;
            _repositoriesHolder = repositoriesHolder;
        }

        public async UniTask Enter()
        {
            await _addressablesAssetsLoader.Initialize();
            await _sdkInitializer.Initialize();
        
            _repositoriesHolder.LoadAll();
        
            _gameStateMachine
                .Enter<LoadSceneState, GameScenes>(GameScenes.Menu)
                .Forget();
        }

        public UniTask Exit() => default;
    }
}