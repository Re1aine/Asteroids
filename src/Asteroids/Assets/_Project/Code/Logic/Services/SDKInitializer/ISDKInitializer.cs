using Cysharp.Threading.Tasks;

namespace _Project.Code.Logic.Services.SDKInitializer
{
    public interface ISDKInitializer
    {
        UniTask Initialize();
        bool IsGamePushInitialized { get; }
        bool IsFireBaseInitialized { get; }
    }
}