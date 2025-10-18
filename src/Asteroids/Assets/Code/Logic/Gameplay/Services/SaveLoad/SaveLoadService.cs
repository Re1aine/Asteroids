using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    public void SetInt(string key, int value) => 
        PlayerPrefs.SetInt(key, value);

    public int GetInt(string key, int defaultValue = 0) => 
        PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : defaultValue;

    public void Save() => 
        PlayerPrefs.Save();
}