namespace Code.Logic.Gameplay.Services.Configs.Configs.GameBalance
{
    public class UfoSpawnerConfig
    {
        public float SpawnCooldown;
        public float MinRadiusSpawn;
        public float MaxRadiusSpawn;

        public UfoSpawnerConfig()
        {
            SpawnCooldown = 5;
            MinRadiusSpawn = 10;
            MaxRadiusSpawn = 12;
        }
    }
}