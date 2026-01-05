using Cysharp.Threading.Tasks;

public interface IMenuUIFactory : IUIFactory
{
    UniTask<MenuWindowPresenter> CreateMenuWindow();
}