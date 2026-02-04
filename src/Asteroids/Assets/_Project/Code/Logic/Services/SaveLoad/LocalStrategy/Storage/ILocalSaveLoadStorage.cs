using _Project.Code.Logic.Services.Repository.Player;

namespace _Project.Code.Logic.Services.SaveLoad.LocalStrategy.Storage
{
    public interface ILocalSaveLoadStorage
    {
        void SetPlayerData(PlayerSaveData data, string key);
        PlayerSaveData GetPlayerData(string fullKey);
    }
}