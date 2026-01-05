using Code.UI.HUD;
using Cysharp.Threading.Tasks;

namespace Code.UI.UIFactory
{
    public interface IUIFactory
    {
        UniTask<AHUDPresenter> CreateHUD();
    }
}