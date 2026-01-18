using System;
using Code.Infrastructure.Common.LogService;
using Code.Logic.Services.SDKInitializer;
using GamePush;
using UnityEngine;

public class GamePushAuthentification : IAuthentification
{
    public event Action AuthStarted;
    public event Action AuthCompleted;
    
    private readonly ISDKInitializer _sdkInitializer;
    private readonly ILogService _logService;

    private bool _isInitialized;
    
    public GamePushAuthentification(ISDKInitializer sdkInitializer, ILogService logService)
    {
        _sdkInitializer = sdkInitializer;
        _logService = logService;
    }
    
    public void Initialize()
    {
        if (_sdkInitializer.IsGamePushInitialized)
        {
            _isInitialized = true;
            _logService.Log("[AuthService initialized successfully]", Color.green, true);
        }
        else
        {
            _isInitialized = false;
            _logService.Log("[AuthService is not initialized]", Color.red, true);                
        }
    }

    public void Login() => 
        ProceedLogin();

    public void Logout() => 
        ProceedLogout();
    
    private bool IsCanAuth()
    {
        if (_isInitialized)
            return true;
        
        return false;
    }

    private void ProceedLogin()
    {
        if(!IsCanAuth())
            return;
        
        if (GP_Player.IsLoggedIn())
        {
            AuthCompleted?.Invoke();
            return;
        }
        
        AuthStarted?.Invoke();
        
        GP_Player.Login();
    }

    private void ProceedLogout()
    {
        if(!IsCanAuth())
            return;
        
        if(!GP_Player.IsLoggedIn())
            return;
        
        GP_Player.Logout();
    }
}