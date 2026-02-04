using _Project.Code.Logic.Services.HUDProvider;
using _Project.Code.UI;
using Cysharp.Threading.Tasks;

namespace _Project.Code.GameFlow.States.Menu
{
    public class SelectSavesState : IState
    {
        private readonly IHUDProvider _hudProvider;

        public SelectSavesState(IHUDProvider hudProvider)
        {
            _hudProvider = hudProvider;
        }

        public UniTask Enter()
        { 
            _hudProvider.HUD.ShowWindow(WindowType.SelectSavesWindow);
            return default;
        }

        public UniTask Exit()
        {
            _hudProvider.HUD.HideWindow(WindowType.SelectSavesWindow);
            return default;
        }
    }
}