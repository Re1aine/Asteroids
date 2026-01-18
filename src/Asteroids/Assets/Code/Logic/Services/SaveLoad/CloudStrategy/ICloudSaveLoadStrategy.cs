using Code.Logic.Services.Repository.Player;
using Cysharp.Threading.Tasks;
using R3;

namespace Code.Logic.Services.SaveLoad.CloudStrategy
{
    public interface ICloudSaveLoadStrategy : ISaveLoadStrategy
    {
        Observable<PlayerSaveData> SyncFallBack { get; }
    }
}