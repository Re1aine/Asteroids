using UnityEngine;

public static class ColorExtensions
{
    public static Color SetAlpha(this Color color, float alpha) => 
        new(color.r, color.g, color.b, alpha);
}