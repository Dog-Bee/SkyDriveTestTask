using System.Collections;
using System.Collections.Generic;
using Core.Level;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatformSpawnerConfig", menuName = "PlatformSpawnerConfig/PlatformSpawnerConfig")]
public class PlatformSpawnerConfig : ScriptableObject
{
    [SerializeField] private PlatformSegment wholePlatform;
    [SerializeField] private List<PlatformSegment> platformPrefab;
    [SerializeField] private int initialSegments = 10;
    [SerializeField] private int safeSegments = 3;
    [SerializeField] private float segmentLength = 10;
    [SerializeField] private float spawnDistance = 60f;
    
    public List<PlatformSegment> PlatformPrefab => platformPrefab;
    public PlatformSegment WholePlatform => wholePlatform;
    public int InitialSegments => initialSegments;
    public int SafeSegments => safeSegments;
    public float SegmentLength => segmentLength;
    public float SpawnDistance => spawnDistance;
    
}
