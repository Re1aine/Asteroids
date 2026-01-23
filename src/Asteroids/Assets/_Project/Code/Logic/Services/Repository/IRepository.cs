using Cysharp.Threading.Tasks;

namespace _Project.Code.Logic.Services.Repository
{
    public interface IRepository
    {
        void Save();
        void Update();
        UniTask Load();
        void Delete();
    }
}