using Code.Logic.Gameplay.Services.PlayerProvider;

namespace Code.UI.PlayerStatsWindow
{
    public class PlayerStatsWindowPresenter
    {
        private readonly PlayerStatsWindowModel _model;
        private readonly PlayerStatsWindowView _view;
    
        private IPlayerProvider _playerProvider;

        public PlayerStatsWindowPresenter(PlayerStatsWindowModel model, PlayerStatsWindowView view)
        {
            _model = model;
            _view = view;
        }
    
        public void Init(IPlayerProvider playerProvider)
        {
            _playerProvider = playerProvider;
        
            _model.Position.OnValueChanged += _view.SetPosition;
            _model.Rotation.OnValueChanged += _view.SetRotation;
            _model.Velocity.OnValueChanged += _view.SetVelocity;
            _model.LaserCharges.OnValueChanged += _view.SetLaserCharges;
            _model.LaserCooldown.OnValueChanged += _view.SetLaserCooldown;

            _view.OnPlayerStatsChanged += SetPlayerStats;
        }

        private void SetPlayerStats()
        {
            _model.SetPosition(_playerProvider.Player.View.transform.position);
            _model.SetRotation(_playerProvider.Player.View.transform.rotation);
            _model.SetVelocity(_playerProvider.Player.View.GetVelocity());
            _model.SetLaserCharges(_playerProvider.Player.View.GetLaserCharges());
            _model.SetLaserCooldown(_playerProvider.Player.View.GetLaserCooldown());
        }

        public void Destroy()
        {
            _model.Position.OnValueChanged -= _view.SetPosition;
            _model.Rotation.OnValueChanged -= _view.SetRotation;
            _model.Velocity.OnValueChanged -= _view.SetVelocity;
            _model.LaserCharges.OnValueChanged -= _view.SetLaserCharges;
            _model.LaserCooldown.OnValueChanged -= _view.SetLaserCooldown;
        
            _view.OnPlayerStatsChanged -= SetPlayerStats;
        
            _view.Destroy();
        }
    }
}