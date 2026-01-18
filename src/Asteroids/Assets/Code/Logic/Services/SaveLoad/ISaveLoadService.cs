using Code.Logic.Services.Repository.Player;
using Cysharp.Threading.Tasks;
using R3;

namespace Code.Logic.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        Observable<Unit> StrategyChanged { get; }
        bool HasConflict { get; }
        ISaveLoadStrategy Current { get; }
        ReadOnlyReactiveProperty<bool> IsAutoMode { get; }
        UniTask Preload();
        void SetPlayerData(PlayerSaveData data);
        UniTask<PlayerSaveData> GetPlayerData();
        UniTask ResolveWithCloud();
        UniTask ResolveWithLocal();
        void UseLocal();
        void UseCloud();
        void ResolveAutomatically();
        void SetAutoMode(bool isActive);
    }
}