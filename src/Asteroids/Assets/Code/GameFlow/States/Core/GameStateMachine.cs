namespace Code.GameFlow.States.Core
{
    public sealed class GameStateMachine : StateMachine
    {
        public GameStateMachine(StateFactory stateFactory) : base(stateFactory)
        {
        }
    }
}