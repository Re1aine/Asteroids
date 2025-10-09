using Code.GameFlow.States.Gameplay;
using VContainer.Unity;

namespace Code.EntryPoints
{
    public class GameplayEntryPoint : IInitializable
    {
        private readonly GameplayStateMachine _gameStateMachine;

        public GameplayEntryPoint(GameplayStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize() => _gameStateMachine.Enter<GameplayStart>();
    }
}
