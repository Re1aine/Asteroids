using Code.Infrastructure.Common.LogService;

namespace Code.GameFlow.States.Gameplay
{
    public sealed class GameplayStateMachine : StateMachine
    {
        public GameplayStateMachine(StateFactory stateFactory, ILogService logService) : base(stateFactory, logService)
        {
        
        }
    }
}