using Code.Infrastructure.Common.LogService;

namespace Code.GameFlow.States.Menu
{
    public sealed class MenuStateMachine : StateMachine
    {
        public MenuStateMachine(StateFactory stateFactory, ILogService logService) : base(stateFactory, logService)
        {
        }
    }
}