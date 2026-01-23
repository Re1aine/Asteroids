namespace Code.Logic.Services.SaveLoad.LocalStrategy
{
    public interface ILocalSaveLoadStrategy : ISaveLoadStrategy
    {
        void InitializeKey();
    }
}