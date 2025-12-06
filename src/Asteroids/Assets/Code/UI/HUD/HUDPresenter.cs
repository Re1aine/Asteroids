namespace Code.UI.HUD
{
    public class HUDPresenter
    {
        public HUDModel Model { get; }
        public HUDView View { get; }
        
        public HUDPresenter(HUDModel model, HUDView view)
        {
            Model = model;
            View = view;
        }

        public void ShowLoseWindow() => 
            Model.ShowLoseWindow();

        public void ShowPlayerStatsWindow() => 
            Model.ShowPlayerStatsWindow();

        public void HidePlayerStatsWindow() => 
            Model.HidePlayerStatsWindow();

        public void HideLoseWindow() => 
            Model.HideLoseWindow();

        public void Destroy()
        {
            Model.Dispose();
            View.Destroy();
        }
    }
}