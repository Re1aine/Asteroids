using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class MenuWindowView : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    
    private MenuStateMachine _menuStateMachine;

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
            .Subscribe(_ => Debug.Log("Exit"))
            .AddTo(this);;
    }

    public void Destroy() => 
        Destroy(gameObject);
}