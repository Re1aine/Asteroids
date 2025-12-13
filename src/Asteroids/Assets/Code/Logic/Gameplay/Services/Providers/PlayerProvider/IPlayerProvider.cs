using System.Threading.Tasks;
using Code.Logic.Gameplay.Entities.Player;

namespace Code.Logic.Gameplay.Services.Providers.PlayerProvider
{
    public interface IPlayerProvider
    {
        PlayerPresenter Player { get; set; }
        Task Initialize();
    }
}