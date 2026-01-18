using Code.Logic.Services.Repository.Player;
using Cysharp.Threading.Tasks;

public interface ISaveLoadStrategy
{
    UniTask SetPlayerData(PlayerSaveData data);
    UniTask<PlayerSaveData> GetPlayerData();
}