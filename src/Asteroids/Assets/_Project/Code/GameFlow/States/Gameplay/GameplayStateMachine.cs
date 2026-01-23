using _Project.Code.Infrastructure.Common.LogService;

namespace _Project.Code.GameFlow.States.Gameplay
{
    public sealed class GameplayStateMachine : StateMachine
    {
        public GameplayStateMachine(StateFactory stateFactory, ILogService logService) : base(stateFactory, logService)
        {
        
        }
    }
}