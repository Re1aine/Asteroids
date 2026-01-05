namespace Code.Logic.Gameplay.Services.Repository
{
    public interface IRepository
    {
        void Save();
        void Update();
        void Load();
        void Delete();
    }
}