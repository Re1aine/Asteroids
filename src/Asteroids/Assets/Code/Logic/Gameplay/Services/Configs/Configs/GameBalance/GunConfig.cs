namespace Code.Logic.Gameplay.Services.ConfigsProvider.Configs.GameBalance
{
    public class GunConfig
    {
        public float BulletCooldown;
        public float LaserShootCooldown;
        public float LaserShootTime;
        public float LaserRange;
        public int LaserChargesMax;
        public int LaserChargesCurrent;
        public int LaserChargeRefillCooldown;

        public GunConfig()
        {
            BulletCooldown = 3;
            LaserShootCooldown = 2;
            LaserShootTime = 2;
            LaserRange = 5;
            LaserChargesMax = 5;
            LaserChargesCurrent = 3;
            LaserChargeRefillCooldown = 5;
        }
    }
}