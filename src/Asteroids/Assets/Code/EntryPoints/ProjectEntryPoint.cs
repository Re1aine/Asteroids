using Code.GameFlow.States.Core;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

public class ProjectEntryPoint : IStartable
{
    private readonly GameStateMachine _gameStateMachine;

    public ProjectEntryPoint(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }
    
    public void Start()
    {
        _gameStateMachine
            .Enter<ProjectInitState>()
            .Forget();
    }
}