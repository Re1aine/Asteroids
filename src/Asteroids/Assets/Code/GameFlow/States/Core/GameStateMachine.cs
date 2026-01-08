using Code.Infrastructure.Common.LogService;

namespace Code.GameFlow.States.Core
{
    public sealed class GameStateMachine : StateMachine
    {
        public GameStateMachine(StateFactory stateFactory, ILogService logService) : base(stateFactory, logService)
        {
        }
    }
}