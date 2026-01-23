using _Project.Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using _Project.Code.Logic.Services.HUDProvider;
using _Project.Code.UI;
using Cysharp.Threading.Tasks;

namespace _Project.Code.GameFlow.States.Menu
{
    public class MenuStartState : IState
    {
        private readonly IHUDProvider _hudProvider;
        private readonly IRepositoriesHolder _repositoriesHolder;

        public MenuStartState(IHUDProvider hudProvider, IRepositoriesHolder repositoriesHolder)
        {
            _hudProvider = hudProvider;
            _repositoriesHolder = repositoriesHolder;
        }

        public UniTask Enter()
        {
            _repositoriesHolder.LoadAll();
        
            _hudProvider.HUD.Build();
        
            _hudProvider.HUD.ShowWindow(WindowType.MenuWindow);
        
            return default;
        }

        public UniTask Exit() => 
            default;
    }
}