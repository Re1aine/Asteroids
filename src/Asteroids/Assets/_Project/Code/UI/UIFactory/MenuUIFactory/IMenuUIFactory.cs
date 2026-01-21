using Code.UI.MenuWindow;
using Code.UI.SelectSavesWindow;
using Cysharp.Threading.Tasks;

namespace Code.UI.UIFactory.MenuUIFactory
{
    public interface IMenuUIFactory : IUIFactory
    {
        UniTask<MenuWindowPresenter> CreateMenuWindow();
        UniTask<SelectSavesWindowPresenter> CreateSelectSavesWindow();
    }
}