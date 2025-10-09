using Code.UI;

namespace Code.Logic.Gameplay.Services.HUDProvider
{
    public interface IHUDProvider
    {
        HUD HUD {get;}   
        void Initialize();
    }
}