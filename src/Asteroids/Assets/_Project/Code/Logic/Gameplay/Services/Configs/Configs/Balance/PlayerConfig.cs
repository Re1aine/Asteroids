namespace _Project.Code.Logic.Gameplay.Services.Configs.Configs.Balance
{
    public class PlayerConfig
    {
        public float DecelerationMove;
        public float AccelerationMove;
        public float MoveSpeed;
        public float RotateSpeed;

        public PlayerConfig()
        {
            DecelerationMove = 1;
            AccelerationMove = 5;
            MoveSpeed = 7;
            RotateSpeed = 200;
        }
    }
}