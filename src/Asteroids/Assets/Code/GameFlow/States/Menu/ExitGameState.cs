using Code.GameFlow.States;
using Cysharp.Threading.Tasks;

public class ExitGameState : IState
{
    private readonly IAuthentification _authentification;

    public ExitGameState(IAuthentification authentification)
    {
        _authentification = authentification;
    }

    public UniTask Enter()
    {
        _authentification.Logout();
        return default;
    }

    public UniTask Exit() => 
        default;
}