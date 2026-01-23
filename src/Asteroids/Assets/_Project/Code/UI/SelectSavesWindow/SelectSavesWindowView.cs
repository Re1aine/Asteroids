using _Project.Code.GameFlow.States.Menu;
using _Project.Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using _Project.Code.Logic.Services.SaveLoad;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Code.UI.SelectSavesWindow
{
    public class SelectSavesWindowView : MonoBehaviour
    {
        [SerializeField] private Button _localSaves;  
        [SerializeField] private Button _cloudSaves;

        private MenuStateMachine _menuStateMachine;
        private IRepositoriesHolder _repositoriesHolder;
        private ISaveLoadService _saveLoadService;

        [Inject]
        public void Construct(MenuStateMachine menuStateMachine, ISaveLoadService saveLoadService)
        {
            _menuStateMachine = menuStateMachine;
            _saveLoadService = saveLoadService;
        }

        private void Start()
        {
            _localSaves.OnClickAsObservable()
                .Subscribe(x => LoadLocalSaves())
                .AddTo(this);
        
            _cloudSaves.OnClickAsObservable()
                .Subscribe(x => LoadCloudSaves())
                .AddTo(this);
        }

        private void LoadCloudSaves()
        {
            _saveLoadService.ResolveWithCloud();
            _menuStateMachine.Enter<MenuStartState>().Forget();
        }

        private void LoadLocalSaves()
        {
            _saveLoadService.ResolveWithLocal();
            _menuStateMachine.Enter<MenuStartState>().Forget();
        }
    
        public void Destroy() => 
            Destroy(gameObject);
    }
}