using Code.UI.HUD;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Services.HUDProvider
{
    public interface IHUDProvider
    {
        AHUDPresenter HUD {get;}   
        UniTask Initialize();
    }
}