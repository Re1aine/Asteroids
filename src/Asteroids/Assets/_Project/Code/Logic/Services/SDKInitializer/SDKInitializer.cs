using System;
using _Project.Code.Infrastructure.Common.LogService;
using Cysharp.Threading.Tasks;
using Firebase;
using GamePush;
using UnityEngine;

namespace _Project.Code.Logic.Services.SDKInitializer
{
    public class SDKInitializer : ISDKInitializer
    {
        private readonly ILogService _logService;
        public bool IsGamePushInitialized {get; private set;}

        public bool IsFireBaseInitialized {get; private set;}

        public SDKInitializer(ILogService logService)
        {
            _logService = logService;
        }

        public async UniTask Initialize()
        {
            await InitializeGamePushSDK();
            //await InitializerFireBaseSDK();
        }

        private UniTask InitializeGamePushSDK()
        {
            try
            {
                //await GP_Init.Ready;
                GP_Game.GameReady();
                _logService.Log("[GamePush initialized successfully]", Color.green, true);
                IsGamePushInitialized = true;
                return default;
            }
            catch (Exception e)
            {
                _logService.LogException(e);
                IsGamePushInitialized = false;
            }          
            
            return default;
        }
        
#if UNITY_EDITOR        
        private async UniTask InitializerFireBaseSDK()
        {
            try
            {
                var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
                if (dependencyStatus != DependencyStatus.Available)
                {
                    _logService.LogError($"Failed to initialize Firebase: {dependencyStatus}");
                    IsFireBaseInitialized = false;;
                }

                _logService.Log("Firebase initialized successfully!", Color.green, true);
                IsFireBaseInitialized = true;;
            }
            catch (Exception e)
            {
                _logService.LogException(e);
                IsFireBaseInitialized = false;
            }
        }
#endif
    }
}