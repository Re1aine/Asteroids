using Code.Logic.Services.Repository.Player;

public interface ILocalSaveLoadStorage
{
    void Initialize();
    void SetPlayerData(PlayerSaveData data, string key);
    PlayerSaveData GetPlayerData(string fullKey);
}