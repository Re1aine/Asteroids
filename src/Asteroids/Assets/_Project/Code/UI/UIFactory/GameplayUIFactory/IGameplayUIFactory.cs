using Code.UI.LoseWindow;
using Code.UI.PlayerStatsWindow;
using Code.UI.ReviveWindow;
using Cysharp.Threading.Tasks;

namespace Code.UI.UIFactory.GameplayUIFactory
{
    public interface IGameplayUIFactory : IUIFactory
    {
        UniTask<LoseWindowPresenter> CreateLoseWindow();

        UniTask<PlayerStatsWindowPresenter> CreatePlayerStatsWindow();

        UniTask<ReviveWindowPresenter> CreateReviveWindow();
    }
}