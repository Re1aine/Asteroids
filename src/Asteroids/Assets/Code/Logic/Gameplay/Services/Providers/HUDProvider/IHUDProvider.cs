using System.Threading.Tasks;
using Code.UI.HUD;

namespace Code.Logic.Gameplay.Services.Providers.HUDProvider
{
    public interface IHUDProvider
    {
        HUDPresenter HUD {get;}   
        void Initialize();
    }
}