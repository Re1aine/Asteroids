namespace Code.Logic.Gameplay.Services.ConfigsProvider.Configs.GameBalance
{
    public class AsteroidSpawnerConfig
    {
        public float SpawnCooldown;
        public float MinRadiusSpawn;
        public float MaxRadiusSpawn;

        public AsteroidSpawnerConfig()
        { 
            SpawnCooldown = 5;
            MinRadiusSpawn = 10;
            MaxRadiusSpawn = 12;
        }
    }
}