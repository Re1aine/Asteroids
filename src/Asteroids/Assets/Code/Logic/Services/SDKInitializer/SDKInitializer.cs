using System;
using Cysharp.Threading.Tasks;
using GamePush;
using UnityEngine;
#if UNITY_EDITOR
using Firebase;
#endif

namespace Code.Logic.Services.SDKInitializer
{
    public class SDKInitializer : ISDKInitializer
    {
        public bool IsGamePushInitialized {get; private set;}
        public bool IsFireBaseInitialized {get; private set;}
    
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
                Debug.Log("<b><color=green> [GamePush initialized successfully] </color></b>");
                IsGamePushInitialized = true;
                return default;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
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
                    Debug.LogError($"Failed to initialize Firebase: {dependencyStatus}");
                    IsFireBaseInitialized = false;;
                }
            
                Debug.Log("<b><color=green> Firebase initialized successfully! </color></b>");
                IsFireBaseInitialized = true;;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                IsFireBaseInitialized = false;
            }
        }
#endif
    }
}