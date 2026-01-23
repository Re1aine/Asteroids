using _Project.Code.Infrastructure.Common.LogService;

namespace _Project.Code.GameFlow.States.Core
{
    public sealed class GameStateMachine : StateMachine
    {
        public GameStateMachine(StateFactory stateFactory, ILogService logService) : base(stateFactory, logService)
        {
        }
    }
}