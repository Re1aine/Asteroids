using _Project.Code.GameFlow.States.Core;
using _Project.Code.GameFlow.States.Menu;
using _Project.Code.Infrastructure.Common.SceneLoader;
using _Project.Code.Logic.Services.Authentification;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Code.UI.MenuWindow
{
    public class MenuWindowView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
    
        private MenuStateMachine _menuStateMachine;
        private IAuthentification _authentification;

        [Inject]
        public void Construct(MenuStateMachine menuStateMachine)
        {
            _menuStateMachine =  menuStateMachine;
        }
    
        private void Start()
        {
            _playButton.OnClickAsObservable()
                .Subscribe(_ => _menuStateMachine.Enter<LoadSceneState, GameScenes>(GameScenes.Gameplay).Forget())
                .AddTo(this);;

            _exitButton.OnClickAsObservable()
                .Subscribe(_ => _menuStateMachine.Enter<ExitGameState>().Forget())
                .AddTo(this);;
        }

        public void Destroy() => 
            Destroy(gameObject);
    }
}