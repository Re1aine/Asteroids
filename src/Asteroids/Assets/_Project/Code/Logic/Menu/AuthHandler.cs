using System;
using Code.GameFlow.States.Menu;
using Code.Infrastructure.Common.LogService;
using Code.Logic.Services.Authentification;
using Cysharp.Threading.Tasks;
using GamePush;
using R3;
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
        
        private readonly CompositeDisposable _disposables = new();

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
    
        public void Initialize() => 
            SetupSubscribes();

        private void SetupSubscribes()
        {
            _authentification.LoginStarted
                .Subscribe(_ => OnLoginStarted())
                .AddTo(_disposables);
        
            _authentification.LoginCompleted
                .Subscribe(_ => OnLoginCompleted())
                .AddTo(_disposables);

            _authentification.LoginFailed
                .Subscribe(_ => OnLoginFailed())
                .AddTo(_disposables);
            
            _authentification.LogoutCompleted
                .Subscribe(_ => OnLogoutComplete())
                .AddTo(_disposables);
            
            _authentification.LogoutFailed
                .Subscribe(_ => OnLogoutFailed())
                .AddTo(_disposables);
        }

        private void OnLoginStarted()
        {
            if (!GP_Platform.IsSecretCodeAuthAvailable())
                return;
        
            _secretCodeDeliverer.SetSecretCode(GP_Player.GetString(AnonymousAuthKey));
            _secretCodeDeliverer.gameObject.SetActive(true);
        }

        private void OnLoginCompleted()
        {
            _secretCodeDeliverer.gameObject.SetActive(false);
            _menuStateMachine.Enter<MenuInitState>().Forget();
        }
        
        private void OnLoginFailed() => 
            _logService.LogError("Login failed");

        private void OnLogoutComplete() => 
            _logService.LogInfo("Logout complete");

        private void OnLogoutFailed() => 
            _logService.LogError("Logout failed");

        public void Dispose() => 
            _disposables?.Dispose();
    }
}