using _Project.Code.GameFlow.States;
using _Project.Code.Infrastructure.Common.LogService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Code.GameFlow
{
    public abstract class StateMachine 
    {
        private readonly StateFactory _stateFactory;
        private readonly ILogService _logService;
        private IExitableState _currentState;

        protected StateMachine(StateFactory stateFactory, ILogService logService)
        {
            _stateFactory = stateFactory;
            _logService = logService;
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

            _logService.LogWithHighlight("[Changing in] " +
                                         $"[{GetType().Name}] " + 
                                         "state from "  +
                                         $"[{_currentState?.GetType().Name ?? "Start"}] -> " +
                                         $"[{typeof(TState).Name}]", 
                Color.white);
            
            TState state = await GetState<TState>();
            
            _currentState = state;
            
            return state;
        }
    }
}