using R3;

namespace _Project.Code.UI.ReviveWindow
{
    public class ReviveWindowPresenter
    {
        public ReviveWindowModel Model {get; }
        public ReviveWindowView View {get; }
    
        private readonly CompositeDisposable _disposables = new();
    
        public ReviveWindowPresenter(ReviveWindowModel model, ReviveWindowView view)
        {
            Model = model;
            View = view;
        
            Model.TimerDuration
                .Subscribe(duration => View.SetTimerDuration(duration))
                .AddTo(_disposables);

            Model.IsTimerActive
                .Subscribe(isActive => View.SetActiveTimer(isActive))
                .AddTo(_disposables);

            View.Accepted
                .Subscribe(_ => Model.Accept())
                .AddTo(_disposables);
        
            View.Declined
                .Subscribe(_ => Model.Decline())
                .AddTo(_disposables);
        }
    
        public void Destroy()
        {
            _disposables.Dispose();
        
            Model.Dispose();
            View.Destroy();
        }
    }
}