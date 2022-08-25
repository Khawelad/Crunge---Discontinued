using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(requiredComponent:typeof(MeshRenderer), requiredComponent2:typeof(MeshFilter), requiredComponent3:typeof(MeshCollider))]
public class GenerateLand : MonoBehaviour
{
    [Header("General Settings")]
    [Range(1, 1000)] public int SizeX;
    [Range(1, 1000)] public int SizeY;
    
    [Header("Point Distribution")]
    [Range(4, 6000)] public int PointDensity;

    
}
