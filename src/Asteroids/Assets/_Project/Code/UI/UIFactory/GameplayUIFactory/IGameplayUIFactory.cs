using _Project.Code.UI.LoseWindow;
using _Project.Code.UI.PlayerStatsWindow;
using _Project.Code.UI.ReviveWindow;
using Cysharp.Threading.Tasks;

namespace _Project.Code.UI.UIFactory.GameplayUIFactory
{
    public interface IGameplayUIFactory : IUIFactory
    {
        UniTask<LoseWindowPresenter> CreateLoseWindow();
        UniTask<PlayerStatsWindowPresenter> CreatePlayerStatsWindow();
        UniTask<ReviveWindowPresenter> CreateReviveWindow();
        UniTask<TipView> CreateTipWindow();
    }
}