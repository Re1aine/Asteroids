using Code.Logic.Gameplay.Services.Repository.Player;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PlayerSaveDataKey = "PlayerSaveData";
        
        public void SetPlayerData(PlayerSaveData data) => 
            PlayerPrefs.SetString(PlayerSaveDataKey, JsonUtility.ToJson(data));

        public PlayerSaveData GetPlayerData(PlayerSaveData defaultValue) => 
            PlayerPrefs.HasKey(PlayerSaveDataKey) ? JsonUtility.FromJson<PlayerSaveData>(PlayerPrefs.GetString(PlayerSaveDataKey)) : defaultValue;

        public void Save() => 
            PlayerPrefs.Save();
    }
}