using Code.Logic.Services.Repository.Player;
using Cysharp.Threading.Tasks;
using R3;

public interface ICloudSaveLoadStrategy : ISaveLoadStrategy
{
    Observable<PlayerSaveData> SyncFallBack { get; }
    UniTask<bool> IsAvailable();
}