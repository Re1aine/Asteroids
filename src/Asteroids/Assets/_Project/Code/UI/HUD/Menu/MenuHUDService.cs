using _Project.Code.UI.MenuWindow;
using _Project.Code.UI.SelectSavesWindow;
using _Project.Code.UI.UIFactory.MenuUIFactory;

namespace _Project.Code.UI.HUD.Menu
{
    public class MenuHUDService :  AHUDService
    {
        private readonly IMenuUIFactory _menuUIFactory;

        private MenuWindowPresenter _menuWindow;
        private SelectSavesWindowPresenter _selectSavesWindow;

        public MenuHUDService(IMenuUIFactory menuUIFactory)
        {
            _menuUIFactory = menuUIFactory;
        }

        public override async void ShowWindow(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.MenuWindow:
                    _menuWindow = await _menuUIFactory.CreateMenuWindow();
                    break;
                case WindowType.SelectSavesWindow:
                    _selectSavesWindow = await _menuUIFactory.CreateSelectSavesWindow();
                    break;
            }
        }

        public override void HideWindow(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.MenuWindow:
                    _menuWindow.Destroy();
                    break;
                case WindowType.SelectSavesWindow:
                    _selectSavesWindow.Destroy();
                    break;
            }
        }
    }
}