using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "CameraConfig/CameraConfig")]
public class CameraConfig : ScriptableObject
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float zoomOffsetZ = -5f;
    [SerializeField] private float zoomDuration = .3f;
    
    public Vector3 Offset => offset;
    public float FollowSpeed => followSpeed;
    public float ZoomOffsetZ => zoomOffsetZ;
    public float ZoomDuration => zoomDuration;
}
