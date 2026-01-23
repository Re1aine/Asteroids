using Cysharp.Threading.Tasks;

namespace _Project.Code.GameFlow.States
{
    public interface IState : IExitableState
    {
        UniTask Enter();
    }
    public interface IStateWithArg<TArg> : IExitableState
    {
        UniTask Enter(TArg arg);
    }

    public interface IExitableState
    {
        UniTask Exit();
    }
}