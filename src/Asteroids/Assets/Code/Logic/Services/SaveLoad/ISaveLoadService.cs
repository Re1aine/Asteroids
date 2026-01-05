using Code.Logic.Services.Repository.Player;

namespace Code.Logic.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void SetPlayerData(PlayerSaveData data);
        PlayerSaveData GetPlayerData();
    }
}