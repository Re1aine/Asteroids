using Code.GameFlow.States;
using Code.Logic.Services.HUDProvider;
using Code.UI;
using Cysharp.Threading.Tasks;

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