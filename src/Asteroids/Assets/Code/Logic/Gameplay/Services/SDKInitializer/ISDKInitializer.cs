using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.SDKInitializer
{
    public interface ISDKInitializer
    {
        UniTask Initialize();
        bool IsGamePushInitialized { get; }
        bool IsFireBaseInitialized { get; }
    }
}