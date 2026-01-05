using System;
using Code.Logic.Gameplay.Entities.Enemy.Asteroid;

namespace Code.Infrastructure.Common.AssetsManagement
{
    public static class AssetsAddress
    {
        public static readonly string Gameplay = "Gameplay";
        public static readonly string UI = "UI";
        
        public static readonly string Player = "Player";
        public static readonly string UFO = "UFO";
        public static readonly string Bullet = "Bullet";
        public static readonly string LaserBeam = "LaserBeam";
        
        public static readonly string GameplayHUD = "GameplayHUD";
        public static readonly string MenuHUD = "MenuHUD";
        
        public static readonly string LoseWindow = "LoseWindow";
        public static readonly string PlayerStatsWindow = "PlayerStatsWindow";
        public static readonly string ReviveWindow = "ReviveWindow";
        
        public static readonly string MenuWindow = "MenuWindow";
        
        public static readonly string AudioConfig = "AudioConfig";
        public static readonly string VFXConfig = "VFXConfig";
        
        public static string GetAddressForAsteroid(AsteroidType type)
        {
            return type switch
            {
                AsteroidType.Asteroid => "Asteroid",
                AsteroidType.AsteroidPart => "AsteroidPart",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}