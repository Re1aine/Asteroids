namespace Code.UI.HUD
{
    public abstract class AHUDPresenter
    {
        public AHUDModel Model { get; }
        public AHUDView View { get; }

        protected AHUDPresenter(AHUDModel model, AHUDView view)
        {
            Model = model;
            View = view;
        }
    
        public void ShowWindow(WindowType windowType) => 
            Model.ShowWindow(windowType);

        public void HideWindow(WindowType windowType) => 
            Model.HideWindow(windowType);
    
        public void Destroy()
        {
            Model.Dispose();
            View.Destroy();
        }
    }
}