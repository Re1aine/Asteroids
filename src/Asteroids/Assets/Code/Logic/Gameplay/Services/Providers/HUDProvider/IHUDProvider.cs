using Code.UI.HUD;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.Providers.HUDProvider
{
    public interface IHUDProvider
    {
        HUDPresenter HUD {get;}   
        UniTask Initialize();
    }
}