using Code.Logic.Services.Repository.Player;
using UnityEngine;

namespace Code.Logic.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PlayerSaveDataKey = "PlayerSaveData";
        
        public void SetPlayerData(PlayerSaveData data) => 
            PlayerPrefs.SetString(PlayerSaveDataKey, JsonUtility.ToJson(data));

        public PlayerSaveData GetPlayerData()
        {
            return PlayerPrefs.HasKey(PlayerSaveDataKey)
                ? JsonUtility.FromJson<PlayerSaveData>(PlayerPrefs.GetString(PlayerSaveDataKey))
                : new PlayerSaveData();
        }
    }
}