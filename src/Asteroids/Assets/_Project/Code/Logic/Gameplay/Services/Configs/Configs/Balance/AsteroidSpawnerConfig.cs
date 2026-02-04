namespace _Project.Code.Logic.Gameplay.Services.Configs.Configs.Balance
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