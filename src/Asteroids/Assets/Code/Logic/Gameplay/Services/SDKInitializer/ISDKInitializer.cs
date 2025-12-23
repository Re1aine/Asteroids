using Cysharp.Threading.Tasks;

public interface ISDKInitializer
{
    UniTask Initialize();
    bool IsGamePushInitialized { get; }
    bool IsFireBaseInitialized { get; }
}