using UnityEngine;

namespace Core.Level
{
    public class PlatformSegment:MonoBehaviour
    {
        [SerializeField] private bool isSafe;
        [SerializeField] private bool isNarrow;
        public bool IsSafe => isSafe;
        public bool IsNarrow =>isNarrow;
    }
}