namespace _Project.Code.UI.HUD.Gameplay
{
    public class GameplayHUDPresenter :  AHUDPresenter
    {
        public new GameplayHUDModel Model { get; }
        public new GameplayHUDView View { get; }
    
        public GameplayHUDPresenter(GameplayHUDModel model, GameplayHUDView view) : base(model, view)
        {
            Model = model;
            View = view;
        }
    }
}