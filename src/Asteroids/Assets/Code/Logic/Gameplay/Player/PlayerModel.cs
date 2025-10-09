using System;

namespace Code.Logic.Gameplay.Player
{
    public class PlayerModel
    {
        public event Action<int> HealthChanged;

        public int Health
        {
            get => _health;
            private set
            {
                _health = value;
                HealthChanged?.Invoke(value);
            }
        }

        private int _health;

        public PlayerModel(int health)
        {
            _health = health;
        }
    
        public void DecrementHealth()
        {
            Health--;
        }
    }
}