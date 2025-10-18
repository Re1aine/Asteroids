using Code.Logic.Gameplay.Services.Repository;

namespace Code.Logic.Gameplay.Services.Holders.RepositoriesHolder
{
    public interface IRepositoriesHolder
    {
        void SaveAll();
        void LoadAll();
        void DeleteAll();
        T GetRepository<T>() where T : IRepository;
    }
}