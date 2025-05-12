using DG.Tweening;
using UnityEngine;

namespace Core.Camera
{
    public class CameraView:MonoBehaviour
    {
        [SerializeField] private ParticleSystem windParticle;
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void BoostParticleState(bool isPlay)
        {
            switch (isPlay)
            {
                case true:
                    windParticle.Play();
                    break;
                case false:
                    windParticle.Stop();
                    break;
            }
        }

        public void CameraShake()
        {
            transform.DOShakePosition(0.5f, 0.3f,fadeOut:true);
        }
    }
}