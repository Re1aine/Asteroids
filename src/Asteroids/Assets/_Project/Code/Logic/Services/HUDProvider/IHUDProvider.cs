using _Project.Code.UI.HUD;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Logic.Services.HUDProvider
{
    public interface IHUDProvider
    {
        AHUDPresenter HUD {get;}   
        UniTask Initialize();
    }
}