using _Project.Code.Logic.Services.Repository.Player;
using R3;

namespace _Project.Code.Logic.Services.SaveLoad.CloudStrategy
{
    public interface ICloudSaveLoadStrategy : ISaveLoadStrategy
    {
        Observable<Unit> SyncCompleted { get; }
        Observable<PlayerSaveData> SyncFailed { get; }
    }
}