using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyConfig", menuName = "DifficultyConfig/DifficultyConfig")]
public class DifficultyConfig : ScriptableObject
{
    [SerializeField] private AnimationCurve spawnChanceCurve;
    [SerializeField] private AnimationCurve emptyPlatformCurve;
    [SerializeField] private AnimationCurve speedMultiplierCurve;
    
    public AnimationCurve SpawnChanceCurve => spawnChanceCurve;
    public AnimationCurve EmptyPlatformCurve => emptyPlatformCurve;
    public AnimationCurve SpeedMultiplierCurve => speedMultiplierCurve;
}
