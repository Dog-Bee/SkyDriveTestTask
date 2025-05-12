using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnvironmentSpawnerConfig", menuName = "EnvironmentSpawnerConfig/EnvironmentSpawnerConfig")]
public class EnvironmentSpawnerConfig : ScriptableObject
{
    [SerializeField] private List<GameObject> environmentPrefabs;

    [SerializeField] private Vector2 leftZone;
    [SerializeField] private Vector2 rightZone;
    [SerializeField] private Vector2 heightRange;
    [SerializeField] private Vector2 forwardRange;
    [SerializeField] private int maxEnvironmentPerPlatform;
    [SerializeField] private int minDistanceBetweenEnvironments;
    [SerializeField] private int attemptsFindSpot;
      
    public List<GameObject> EnvironmentPrefabs => environmentPrefabs;
    public Vector2 LeftZone => leftZone;
    public Vector2 RightZone => rightZone;
    public Vector2 HeightRange => heightRange;
    public Vector2 ForwardRange => forwardRange;
    public int MaxEnvironmentPerPlatform => maxEnvironmentPerPlatform;
    public int MinDistanceBetweenEnvironments => minDistanceBetweenEnvironments;
    public int AttemptsFindSpot => attemptsFindSpot;
}
