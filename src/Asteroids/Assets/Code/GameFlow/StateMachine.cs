using Code.GameFlow.States;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.GameFlow
{
    public abstract class StateMachine 
    {
        private readonly StateFactory _stateFactory;
        private IExitableState _currentState;

        protected StateMachine(StateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }
    
        public async UniTask Enter<TState>() where TState : IState
        {
            IState state = await ChangeState<TState>();
            await state.Enter();
        }

        public async UniTask Enter<TState, TArg>(TArg arg) where TState : IStateWithArg<TArg>
        {
            TState state = await ChangeState<TState>();
            await state.Enter(arg);
        }

        private async UniTask<TState> GetState<TState>() where TState : IExitableState => 
            await _stateFactory.Create<TState>();

        private async UniTask<TState> ChangeState<TState>() where TState : IExitableState
        {
            if (_currentState != null) 
                await _currentState.Exit();
            
            Debug.Log("Changing in " +
                      $"<color=white><b>{GetType().Name}</color><b> " + 
                      "state from "  +
                      $"<color=white><b>{_currentState?.GetType().Name ?? "Start"}" +
                      $" -> {typeof(TState).Name}</color><b>");
            
            TState state = await GetState<TState>();
            
            _currentState = state;
            
            return state;
        }
    }
}