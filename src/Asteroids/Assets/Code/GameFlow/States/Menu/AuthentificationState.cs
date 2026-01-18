using Code.GameFlow.States;
using Cysharp.Threading.Tasks;

#if !UNITY_EDITOR && UNITY_WEBGL
using UnityEngine;
#endif

public class AuthentificationState : IState
{
    private readonly IAuthentification _authentification;
    private readonly AuthHandler _authHandler;

    public AuthentificationState(IAuthentification authentification, AuthHandler authHandler)
    {
        _authentification = authentification;
        _authHandler = authHandler;
    }

    public UniTask Enter()
    {
        _authentification.Initialize();
        _authHandler.Initialize();
        
        _authentification.Login();
        
        return default;
    }

    public UniTask Exit() => 
        default;
}