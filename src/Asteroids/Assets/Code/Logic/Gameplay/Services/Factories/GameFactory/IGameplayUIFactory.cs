using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.UI.LoseWindow;
using Code.UI.PlayerStatsWindow;
using Cysharp.Threading.Tasks;

public interface IGameplayUIFactory : IUIFactory
{
    UniTask<LoseWindowPresenter> CreateLoseWindow();

    UniTask<PlayerStatsWindowPresenter> CreatePlayerStatsWindow();

    UniTask<ReviveWindowPresenter> CreateReviveWindow();
}