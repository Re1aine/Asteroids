
namespace Code.UI.HUD
{
    public class HUDPresenter
    {
        public HUDModel Model { get; }
        public HUDView View { get; }
        
        private readonly HUDService _hudService;

        public HUDPresenter(HUDModel model, HUDView view, HUDService hudService)
        {
            Model = model;
            View = view;
            
            _hudService = hudService;
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
            _hudService.Dispose();
            Model.Dispose();
            View.Destroy();
        }
    }
}