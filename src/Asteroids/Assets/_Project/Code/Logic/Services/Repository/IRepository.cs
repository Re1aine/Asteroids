using Cysharp.Threading.Tasks;

namespace Code.Logic.Services.Repository
{
    public interface IRepository
    {
        void Save();
        void Update();
        UniTask Load();
        void Delete();
    }
}