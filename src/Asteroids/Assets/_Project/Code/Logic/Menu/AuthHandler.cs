using System;
using Code.GameFlow.States.Menu;
using Code.Infrastructure.Common.LogService;
using Code.Logic.Services.Authentification;
using Cysharp.Threading.Tasks;
using GamePush;
using VContainer.Unity;

namespace Code.Logic.Menu
{
    public class AuthHandler : IInitializable, IDisposable
    {
        private const string AnonymousAuthKey = "secretCode";

        private readonly IAuthentification _authentification;
        private readonly SecretCodeDeliverer _secretCodeDeliverer;
        private readonly MenuStateMachine _menuStateMachine;
        private readonly ILogService _logService;

        public AuthHandler(IAuthentification authentification,
            SecretCodeDeliverer secretCodeDeliverer,
            MenuStateMachine menuStateMachine,
            ILogService logService)
        {
            _authentification = authentification;
            _secretCodeDeliverer = secretCodeDeliverer;
            _menuStateMachine = menuStateMachine;
            _logService = logService;
        }
    
        public void Initialize()
        {
            _authentification.AuthStarted += OnAuthStarted;
            _authentification.AuthCompleted += OnAuthCompleted;
        
            GP_Player.OnLoginComplete += OnLoginComplete;
            GP_Player.OnLoginError += OnLoginError;
        
            GP_Player.OnLogoutComplete += OnLogoutComplete;
            GP_Player.OnLogoutError += OnLogoutError;
        }

        private void OnAuthStarted()
        {
            if (!GP_Platform.IsSecretCodeAuthAvailable())
                return;
        
            _secretCodeDeliverer.SetSecretCode(GP_Player.GetString(AnonymousAuthKey));
            _secretCodeDeliverer.gameObject.SetActive(true);
        }

        private void OnAuthCompleted()
        {
            _secretCodeDeliverer.gameObject.SetActive(false);
            _menuStateMachine.Enter<MenuInitState>().Forget();
        }
    
        private void OnLoginComplete()
        {
            _logService.LogInfo("Login complete");
            OnAuthCompleted();
        }
    
        private void OnLoginError() => 
            _logService.LogError("Login error");

        private void OnLogoutComplete()
        {
        
        }

        private void OnLogoutError()
        {
        
        }

        public void Dispose()
        {
            _authentification.AuthStarted -= OnAuthStarted;
            _authentification.AuthCompleted -= OnAuthCompleted;
        
            GP_Player.OnLoginComplete -= OnLoginComplete;
            GP_Player.OnLoginError -= OnLoginError;
        
            GP_Player.OnLogoutComplete -= OnLogoutComplete;
            GP_Player.OnLogoutError -= OnLogoutError;
        }
    }
}