using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleSpawnerConfig", menuName = "ObstacleSpawnerConfig/ObstacleSpawnerConfig")]
public class ObstacleSpawnerConfig : ScriptableObject
{
    [SerializeField] private List<GameObject> obstaclePrefabs;
    [SerializeField] private int gridWidth=3;
    [SerializeField] private float spacing=13;
    [SerializeField] private float zOffsetSpacing=20;
    
    public List<GameObject> ObstaclePrefabs => obstaclePrefabs;
    public int GridWidth => gridWidth;
    public float Spacing => spacing;
    public float ZOffsetSpacing => zOffsetSpacing;
}
