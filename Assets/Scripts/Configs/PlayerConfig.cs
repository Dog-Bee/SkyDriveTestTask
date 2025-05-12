using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "PlayerConfig/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] private float baseSpeed = 10f;
    [SerializeField] private float boostMultiplier = 2f;
    [SerializeField] private float tiltAngle = 25f;
    [SerializeField] private float fallAngle = 45f;
    [SerializeField] private float lateralSpeed = 5f;
    [SerializeField] private float maxX = 20f;
    [SerializeField] private float downSpeed = 25f;
    [SerializeField] private float minY = -5f;
    
    public float BaseSpeed =>baseSpeed;
    public float BoostMultiplier =>boostMultiplier;
    public float TiltAngle => tiltAngle;
    public float FallAngle => fallAngle;
    public float LateralSpeed => lateralSpeed;
    public float MaxX => maxX;
    public float DownSpeed => downSpeed;
    public float MinY => minY;
}
