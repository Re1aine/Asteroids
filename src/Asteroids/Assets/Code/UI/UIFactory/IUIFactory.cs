using Cysharp.Threading.Tasks;

public interface IUIFactory
{
    UniTask<AHUDPresenter> CreateHUD();
}