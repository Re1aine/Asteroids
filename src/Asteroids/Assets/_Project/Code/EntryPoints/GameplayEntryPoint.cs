using _Project.Code.GameFlow.States.Gameplay;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace _Project.Code.EntryPoints
{
    public class GameplayEntryPoint : IStartable
    {
        private readonly GameplayStateMachine _gameplayStateMachine;

        public GameplayEntryPoint(GameplayStateMachine gameplayStateMachine) => 
            _gameplayStateMachine = gameplayStateMachine;

        public void Start()
        {
            _gameplayStateMachine
                .Enter<GameplayInitState>()
                .Forget();
        }
    }
}
