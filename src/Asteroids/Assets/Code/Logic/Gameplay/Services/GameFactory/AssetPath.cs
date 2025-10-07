using System;

public static class AssetPath
{
    public static readonly string Player = "Player";
    public static readonly string UFO = "UFO";
    public static readonly string Bullet = "Bullet";
    public static readonly string LaserBeam = "LaserBeam";

    public static readonly string HUD = "UI/HUD";
    public static readonly string LoseWindow = "UI/LoseWindow";
    public static readonly string PlayerStatsWindow = "UI/PlayerStatsWindow";

    public static string GetPathForAsteroid(AsteroidType type)
    {
        return type switch
        {
            AsteroidType.Asteroid => "Asteroids/Asteroid",
            AsteroidType.AsteroidPart => "Asteroids/AsteroidPart",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}