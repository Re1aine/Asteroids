using Code.UI;

namespace Code.Logic.Gameplay.Services.HUDProvider
{
    public interface IHUDProvider
    {
        HUDPresenter HUD {get;}   
        void Initialize();
    }
}