using Code.GameFlow.States;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Code.GameFlow
{
    public class StateFactory
    {
        private readonly IObjectResolver _resolver;
    
        public StateFactory(IObjectResolver resolver) => 
            _resolver = resolver;
    
        public UniTask<TState> Create<TState>() where TState : IExitableState => 
            new(_resolver.ResolveInstance<TState>());
    }
}