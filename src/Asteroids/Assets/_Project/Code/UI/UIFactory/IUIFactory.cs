using _Project.Code.UI.HUD;
using Cysharp.Threading.Tasks;

namespace _Project.Code.UI.UIFactory
{
    public interface IUIFactory
    {
        UniTask<AHUDPresenter> CreateHUD();
    }
}