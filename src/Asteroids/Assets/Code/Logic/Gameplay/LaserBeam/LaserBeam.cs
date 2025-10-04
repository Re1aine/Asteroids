using System;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [field: SerializeField] public LineRenderer LineRenderer {get; private set;}
    [SerializeField] private float _distance;

    public void ClearAllLinePositions() => LineRenderer.positionCount = 0;
}

