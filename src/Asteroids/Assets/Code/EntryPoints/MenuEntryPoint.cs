using Cysharp.Threading.Tasks;
using VContainer.Unity;
public class MenuEntryPoint : IStartable
{
    private readonly MenuStateMachine _menuStateMachine;

    public MenuEntryPoint(MenuStateMachine menuStateMachine)
    {
        _menuStateMachine = menuStateMachine;
    }
    
    public void Start()
    {
        _menuStateMachine
            .Enter<MenuInitState>()
            .Forget();
    }
}