using Code.Logic.Services.Repository.Player;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Services.SaveLoad
{
    public interface ISaveLoadStrategy
    {
        UniTask SetPlayerData(PlayerSaveData data);
        UniTask<PlayerSaveData> GetPlayerData();
    }
}