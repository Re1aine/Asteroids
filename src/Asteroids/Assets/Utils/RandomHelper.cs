using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class RandomHelper
{
    public static T GetRandomEnumValue<T>(bool exceptNone = false) where T : Enum
    {
        Array enumValues = Enum.GetValues(typeof(T));
        int randomIndex = Random.Range(exceptNone ? 1 : 0, enumValues.Length);
        return (T)enumValues.GetValue(randomIndex);
    }

    public static Vector3 GetRandomPositionInCollider(Collider collider)
    {
        float x = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        float y = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
        float z = Random.Range(collider.bounds.min.z, collider.bounds.max.z);
        
        return new Vector3(x, y, z);
    } 
    
    public static Vector3 GetRandomPositionInCollider(Collider collider, bool excludeX, bool excludeY, bool excludeZ)
    {
        Vector3 min = collider.bounds.min;
        Vector3 max = collider.bounds.max;
    
        float x = excludeX ? collider.transform.position.x : Random.Range(min.x, max.x);
        float y = excludeY ? collider.transform.position.y : Random.Range(min.y, max.y);
        float z = excludeZ ? collider.transform.position.z : Random.Range(min.z, max.z);
        
        return new Vector3(x, y, z);
    }
    
    public static Quaternion GetRandomRotation(bool excludeZ = false)
    {
        float x = Random.Range(0f, 360f);
        float y = Random.Range(0f, 360f);
        float z = excludeZ ? 0 : Random.Range(0f, 360f);
        
        return new Quaternion(x, y, z, 0f);
    }

    public static GameObject GetRandomGameObject(this List<GameObject> collection) => 
        collection[Random.Range(0, collection.Count)];
    
    public static char GetRandomChar(this List<char> list) => 
        list[Random.Range(0, list.Count)];

    public static List<char> RemoveRandomChar(this List<char> list)
    {
        list.RemoveAt(Random.Range(0, list.Count));
        return list;
    }

    public static char GetRandomAlphabetLetter() => 
        (char)('A' + Random.Range(0, 26));

    public static IList<T> Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }
    
    public static Vector2 GetRandomPointOnCircle(Vector2 center, float minRadius, float maxRadius)
    {
        minRadius = Mathf.Max(0, minRadius);
        maxRadius = Mathf.Max(minRadius, maxRadius);
        
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float radius = Random.Range(minRadius, maxRadius);
        
        float x = center.x + radius * Mathf.Cos(angle);
        float y = center.y + radius * Mathf.Sin(angle);

        return new Vector2(x, y);
    }
    
    public static Vector3 GetRandomPositionInsideBounds(Vector2 min, Vector2 max) => 
        new(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0f);
}