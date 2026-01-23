using R3;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.ReviveWindow
{
    public class ReviveWindowView : MonoBehaviour
    {
        public Observable<Unit> Accepted => _accepted;
        public Observable<Unit> Declined => _declined;
    
        private readonly Subject<Unit> _accepted = new();
        private readonly Subject<Unit> _declined = new();
    
        [SerializeField] private Timer _timer;
        [SerializeField] private Button _declineButton;
        [SerializeField] private Button _acceptButton;
    
        private void Awake()
        {
            _acceptButton.OnClickAsObservable()
                .Subscribe(_ => _accepted.OnNext(Unit.Default))
                .AddTo(this);

            _declineButton.OnClickAsObservable()
                .Subscribe(_ => _declined.OnNext(Unit.Default))
                .AddTo(this);
        
            _timer.Ended
                .Subscribe(_ => _declined.OnNext(Unit.Default))
                .AddTo(this);
        }
    
        public void SetActiveTimer(bool isActive)
        {
            if (isActive)
                _timer.Run();
            else
                _timer.Stop();
        }

        public void SetTimerDuration(float duration) => 
            _timer.SetDuration(duration);
    
        public void Destroy() => 
            Destroy(gameObject);
    }
}