using Code.Logic.Services.Authentification;
using Cysharp.Threading.Tasks;

namespace Code.GameFlow.States.Menu
{
    public class AuthentificationState : IState
    {
        private readonly IAuthentification _authentification;

        public AuthentificationState(IAuthentification authentification)
        {
            _authentification = authentification;
        }

        public UniTask Enter()
        {
            _authentification.Initialize();
            _authentification.Login();
            
            return default;
        }

        public UniTask Exit() => 
            default;
    }
}