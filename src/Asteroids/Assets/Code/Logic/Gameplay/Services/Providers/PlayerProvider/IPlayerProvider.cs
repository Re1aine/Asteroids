using Code.Logic.Gameplay.Entities.Player;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.Providers.PlayerProvider
{
    public interface IPlayerProvider
    {
        PlayerPresenter Player { get; set; }
        UniTask Initialize();
    }
}