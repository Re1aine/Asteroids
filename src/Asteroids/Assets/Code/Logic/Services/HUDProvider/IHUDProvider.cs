using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.Providers.HUDProvider
{
    public interface IHUDProvider
    {
        AHUDPresenter HUD {get;}   
        UniTask Initialize();
    }
}