using R3;

namespace Code.Logic.Services.Authentification
{
    public interface IAuthentification
    {
        Observable<Unit> LoginStarted { get; }
        Observable<Unit> LoginCompleted { get; }
        Observable<Unit> LoginFailed { get; }
        Observable<Unit> LogoutCompleted { get; }
        Observable<Unit> LogoutFailed { get; }
        void Initialize();
        void Login();
        void Logout();
    }
}