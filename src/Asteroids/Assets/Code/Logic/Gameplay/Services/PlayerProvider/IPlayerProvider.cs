using Code.Logic.Gameplay.Player;

namespace Code.Logic.Gameplay.Services.PlayerProvider
{
    public interface IPlayerProvider
    {
        PlayerPresenter Player { get; set; }
        void Initialize();
    }
}