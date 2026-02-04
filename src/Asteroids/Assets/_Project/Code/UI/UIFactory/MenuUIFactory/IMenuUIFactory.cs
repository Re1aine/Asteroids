using _Project.Code.UI.MenuWindow;
using _Project.Code.UI.SelectSavesWindow;
using Cysharp.Threading.Tasks;

namespace _Project.Code.UI.UIFactory.MenuUIFactory
{
    public interface IMenuUIFactory : IUIFactory
    {
        UniTask<MenuWindowPresenter> CreateMenuWindow();
        UniTask<SelectSavesWindowPresenter> CreateSelectSavesWindow();
    }
}