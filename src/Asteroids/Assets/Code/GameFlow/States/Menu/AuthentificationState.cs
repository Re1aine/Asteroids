using Code.Logic.Menu;
using Code.Logic.Services.Authentification;
using Cysharp.Threading.Tasks;
#if !UNITY_EDITOR && UNITY_WEBGL
using UnityEngine;
#endif

namespace Code.GameFlow.States.Menu
{
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
}