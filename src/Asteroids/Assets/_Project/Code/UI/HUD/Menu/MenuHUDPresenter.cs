namespace _Project.Code.UI.HUD.Menu
{
    public class MenuHUDPresenter : AHUDPresenter
    {
        public new MenuHUDModel Model { get; }
        public new MenuHUDView View { get; }

        public MenuHUDPresenter(MenuHUDModel model, MenuHUDView view) : base(model, view)
        {
            Model = model;
            View = view;
        }
    }
}