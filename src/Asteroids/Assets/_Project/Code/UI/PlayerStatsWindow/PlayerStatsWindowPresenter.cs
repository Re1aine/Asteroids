using R3;

namespace _Project.Code.UI.PlayerStatsWindow
{
    public class PlayerStatsWindowPresenter
    {
        public PlayerStatsWindowModel Model {get; }
        public PlayerStatsWindowView View {get; }
        
        private readonly CompositeDisposable _disposables = new();
        
        public PlayerStatsWindowPresenter(PlayerStatsWindowModel model, PlayerStatsWindowView view)
        {
            Model = model;
            View = view;
            
            Model.Position
                .Subscribe(position => View.SetPosition(position))
                .AddTo(_disposables);
            
            Model.Rotation
                .Subscribe(rotation => View.SetRotation(rotation))
                .AddTo(_disposables);
            
            Model.Velocity
                .Subscribe(velocity => View.SetVelocity(velocity))
                .AddTo(_disposables);
            
            Model.LaserCharges
                .Subscribe(charges  => View.SetLaserCharges(charges))
                .AddTo(_disposables);
            
            Model.LaserCooldown
                .Subscribe(cooldown => View.SetLaserCooldown(cooldown))
                .AddTo(_disposables);
        }
        
        public void Destroy()
        { 
            _disposables.Dispose();
            Model.Dispose();
            View.Destroy().Forget();
        }
    }
}