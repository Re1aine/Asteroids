using R3;

namespace _Project.Code.UI.HUD
{
    public abstract class AHUDPresenter
    {
        public AHUDModel Model { get; }
        public AHUDView View { get; }
        
        private readonly CompositeDisposable _disposables = new();

        protected AHUDPresenter(AHUDModel model, AHUDView view)
        {
            Model = model;
            View = view;

            Model.Builded
                .Subscribe(x => View.Build())
                .AddTo(_disposables);
        }
    
        public void ShowWindow(WindowType windowType) => 
            Model.ShowWindow(windowType);

        public void HideWindow(WindowType windowType) => 
            Model.HideWindow(windowType);

        public void Build() => 
            Model.Build();

        public void Destroy()
        {
            _disposables.Dispose();
            
            Model.Dispose();
            View.Destroy();
        }
    }
}