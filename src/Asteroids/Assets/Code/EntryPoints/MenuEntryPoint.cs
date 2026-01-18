using Code.GameFlow.States.Menu;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Code.EntryPoints
{
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
                .Enter<AuthentificationState>()
                .Forget();
        }
    }
}