using _Project.Code.Infrastructure.Common.LogService;

namespace _Project.Code.GameFlow.States.Menu
{
    public sealed class MenuStateMachine : StateMachine
    {
        public MenuStateMachine(StateFactory stateFactory, ILogService logService) : base(stateFactory, logService)
        {
        }
    }
}