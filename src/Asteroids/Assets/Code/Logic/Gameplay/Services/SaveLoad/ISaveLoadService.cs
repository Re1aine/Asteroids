public interface ISaveLoadService
{
    void SetInt(string key, int value);
    int GetInt(string key, int value = 0);
    void Save();
}