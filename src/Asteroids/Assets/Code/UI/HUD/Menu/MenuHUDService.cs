public class MenuHUDService :  AHUDService
{
    private readonly IMenuUIFactory _menuUIFactory;

    private MenuWindowPresenter _menuWindow;
    
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
        }
    }

    public override void HideWindow(WindowType windowType)
    {
        switch (windowType)
        {
            case WindowType.MenuWindow:
                _menuWindow.Destroy();
                break;
        }
    }
}