using _Project.Code.Infrastructure.Common.AssetsManagement;
using _Project.Code.Infrastructure.Common.AssetsManagement.AssetLoader;
using _Project.Code.Infrastructure.Common.SceneLoader;
using _Project.Code.Logic.Gameplay.Audio;
using _Project.Code.Logic.Gameplay.Services.Configs.AssetsConfigProvider;
using _Project.Code.Logic.Services.SDKInitializer;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Code.GameFlow.States.Core
{
    public class ProjectInitState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAddressablesAssetsLoader _addressablesAssetsLoader;
        private readonly ISDKInitializer _sdkInitializer;
        private readonly IAssetsConfigsProvider _assetsConfigsProvider;
        private readonly IAudioService _audioService;

        public ProjectInitState(GameStateMachine gameStateMachine,
            IAddressablesAssetsLoader addressablesAssetsLoader,
            ISDKInitializer sdkInitializer, IAssetsConfigsProvider assetsConfigsProvider, IAudioService audioService)
        {
            _gameStateMachine = gameStateMachine;
            _addressablesAssetsLoader = addressablesAssetsLoader;
            _sdkInitializer = sdkInitializer;
            _assetsConfigsProvider = assetsConfigsProvider;
            _audioService = audioService;
        }

        public async UniTask Enter()
        {
            await _addressablesAssetsLoader.Initialize();
            await _sdkInitializer.Initialize();

            await _assetsConfigsProvider.Initialize();
            
            await _addressablesAssetsLoader.LoadAssetsByLabels<GameObject>(
                AssetsAddress.Shared,
                AssetsAddress.Menu);
            
            _audioService.Initialize();
            
            _gameStateMachine
                .Enter<LoadSceneState, GameScenes>(GameScenes.Menu)
                .Forget();
        }

        public UniTask Exit() => default;
    }
}