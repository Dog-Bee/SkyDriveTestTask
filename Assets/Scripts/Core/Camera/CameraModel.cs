
using UnityEngine;

namespace Core.Camera
{
    public class CameraModel
    {  
        private readonly Vector3 _offset;
        private readonly float _followSpeed = 5f;
        private readonly float _zoomOffsetZ = -5f;
        private readonly float _zoomDuration = .3f;
        
         public Vector3 Offset=>_offset;
         public  float FollowSpeed => _followSpeed;
         public  float ZoomOffsetZ => _zoomOffsetZ;
         public  float ZoomDuration => _zoomDuration;

         public CameraModel(CameraConfig config)
         {
             _offset = config.Offset;
             _followSpeed = config.FollowSpeed;
             _zoomOffsetZ = config.ZoomOffsetZ;
             _zoomDuration = config.ZoomDuration;
         }
    }
}