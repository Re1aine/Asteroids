using _Project.Code.Logic.Services.Repository.Player;
using Cysharp.Threading.Tasks;
using R3;

namespace _Project.Code.Logic.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        Observable<Unit> StrategyChanged { get; }
        bool HasConflict { get; }
        ISaveLoadStrategy CurrentStrategy { get; }
        ReadOnlyReactiveProperty<bool> IsAutoMode { get; }
        UniTask Preload();
        void SetPlayerData(PlayerSaveData data);
        UniTask<PlayerSaveData> GetPlayerData();
        UniTask ResolveWithCloud();
        UniTask ResolveWithLocal();
        void UseLocalStrategy();
        void UseCloudStrategy();
        void ResolveAutomatically();
        void SetAutoMode(bool isActive);
    }
}