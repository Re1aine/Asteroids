using Code.Infrastructure.Common.LogService;
using Code.Logic.Services.SDKInitializer;
using GamePush;
using R3;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Logic.Services.Authentification
{
    public class GamePushAuthentification : IAuthentification
    {
        public Observable<Unit> LoginStarted => _loginStarted;
        public Observable<Unit> LoginCompleted => _loginCompleted;
        public Observable<Unit> LoginFailed => _loginFailed;
        public Observable<Unit> LogoutCompleted => _logoutCompleted;
        public Observable<Unit> LogoutFailed => _logoutFailed;

        private readonly Subject<Unit> _loginStarted = new();
        private readonly Subject<Unit> _loginCompleted = new();
        private readonly Subject<Unit> _loginFailed = new();
        private readonly Subject<Unit> _logoutCompleted = new();
        private readonly Subject<Unit> _logoutFailed = new();
        
        private readonly ISDKInitializer _sdkInitializer;
        private readonly ILogService _logService;

        private bool _isInitialized;
        
        private readonly CompositeDisposable _disposables = new();
        
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
                SetupSubscribes();
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
                _loginCompleted.OnNext(Unit.Default);
                return;
            }
            
            _loginStarted.OnNext(Unit.Default);
        
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

        private void SetupSubscribes()
        {
            Observable.FromEvent(
                    h => new UnityAction(h),
                    h => GP_Player.OnLoginComplete += h,
                    h => GP_Player.OnLoginComplete -= h
                )
                .Subscribe(_ => _loginCompleted.OnNext(Unit.Default))
                .AddTo(_disposables);
                
            Observable.FromEvent(
                    h => new UnityAction(h),
                    h => GP_Player.OnLoginError += h,
                    h => GP_Player.OnLoginError -= h
                )
                .Subscribe(_ => _loginFailed.OnNext(Unit.Default))
                .AddTo(_disposables);
                
            Observable.FromEvent(
                    h => new UnityAction(h),
                    h => GP_Player.OnLogoutComplete += h,
                    h => GP_Player.OnLogoutComplete -= h
                )
                .Subscribe(_ => _logoutCompleted.OnNext(Unit.Default))
                .AddTo(_disposables);

            Observable.FromEvent(
                    h => new UnityAction(h),
                    h => GP_Player.OnLogoutError += h,
                    h => GP_Player.OnLogoutError -= h
                )
                .Subscribe(_ => _logoutFailed.OnNext(Unit.Default))
                .AddTo(_disposables);
        }
    }
}