public class MenuWindowPresenter
{
    public MenuWindowModel Model { get; }
    public MenuWindowView View { get; }

    public MenuWindowPresenter(MenuWindowModel model, MenuWindowView view)
    {
        Model = model;
        View = view;
    }

    public void Destroy()
    {
        Model.Dispose();
        View.Destroy();
    }
    
}