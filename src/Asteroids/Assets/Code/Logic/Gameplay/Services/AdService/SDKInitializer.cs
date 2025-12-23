using System;
using Cysharp.Threading.Tasks;
using Firebase;
using GamePush;
using UnityEngine;

public class SDKInitializer : ISDKInitializer
{
    private FirebaseApp _firebaseApp;

    public bool IsGamePushInitialized {get; private set;}
    public bool IsFireBaseInitialized {get; private set;}
    
    public async UniTask Initialize()
    {
        await InitializeGamePushSDK();
        await InitializerFireBaseSDK();
    }

    private async UniTask InitializeGamePushSDK()
    {
        try
        {
            await GP_Init.Ready;
            Debug.Log("<b><color=green> [GamePush initialized successfully] </color></b>");
            IsGamePushInitialized = true;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            IsGamePushInitialized = false;
        }          
    }
    

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
       
            _firebaseApp = FirebaseApp.DefaultInstance;
            Debug.Log("<b><color=green> Firebase initialized successfully! </color></b>");
            IsFireBaseInitialized = true;;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            IsFireBaseInitialized = false;
        }
    } 
}