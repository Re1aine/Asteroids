using Code.Logic.Gameplay.Services.Repository.Player;

namespace Code.Logic.Gameplay.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void SetPlayerData(PlayerSaveData data);
        PlayerSaveData GetPlayerData();
    }
}