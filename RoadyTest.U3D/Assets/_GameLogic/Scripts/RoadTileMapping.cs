using System;
using UnityEngine;

public class RoadTileMapping : MonoBehaviour
{
    [SerializeField] public RoadTileMap[] TileMaps;
}

[Serializable]
public class RoadTileMap
{
    [SerializeField] public int Id;
    [SerializeField] public string Name;
    [SerializeField] public GameObject TilePrefab;
    [SerializeField] public float Rotation;
}