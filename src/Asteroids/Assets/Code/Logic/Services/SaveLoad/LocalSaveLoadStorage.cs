using Code.Logic.Services.Repository.Player;
using UnityEngine;

public class LocalSaveLoadStorage : ILocalSaveLoadStorage
{
    private const string AllPlayersSavesDataKey = "PlayersSavesData";

    private PlayersSavesData _players;
    
    public void Initialize()
    {
        _players = PlayerPrefs.HasKey(AllPlayersSavesDataKey)
            ? JsonUtility.FromJson<PlayersSavesData>(PlayerPrefs.GetString(AllPlayersSavesDataKey))
            : new PlayersSavesData();
    }

    public void SetPlayerData(PlayerSaveData data, string key)
    {
        string id = FormatToKey(key);
        
        var index = _players.Datas.FindIndex(x => string.Equals(x.Key, id));
        
        if (index >= 0)
            _players.Datas[index].Data = data;
        else
            _players.Datas.Add(new PlayersSavesDataWrapper()
            {
                Key = id,
                Data = data,
            });

        PlayerPrefs.SetString(AllPlayersSavesDataKey, JsonUtility.ToJson(_players));
    }

    public PlayerSaveData GetPlayerData(string fullKey)
    {
        string key = FormatToKey(fullKey);
        
        if(HasPlayer(fullKey))
            return _players.Datas
                .Find(x => string.Equals(x.Key, key))
                .Data;
        
        PlayersSavesDataWrapper dataWrapper = new PlayersSavesDataWrapper()
        {
            Key = key,
            Data = new PlayerSaveData()
        };
        
        _players.Datas.Add(dataWrapper);
        
        return dataWrapper.Data;
    }

    private bool HasPlayer(string key) => 
        _players.Datas.Exists(x => string.Equals(x.Key, FormatToKey(key)));

    private string FormatToKey(string key)
    {
        if (string.IsNullOrEmpty(key))
            return string.Empty;
    
        int lastUnderscore = key.LastIndexOf('_');
    
        if (lastUnderscore >= 0 && lastUnderscore < key.Length - 1)
            return key.Substring(lastUnderscore + 1);
    
        return key;
    }
}