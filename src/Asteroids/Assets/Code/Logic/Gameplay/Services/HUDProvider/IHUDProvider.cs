using Code.UI;
using Code.UI.HUD;

namespace Code.Logic.Gameplay.Services.HUDProvider
{
    public interface IHUDProvider
    {
        HUDPresenter HUD {get;}   
        void Initialize();
    }
}