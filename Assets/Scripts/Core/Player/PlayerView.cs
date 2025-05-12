using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Core.Player
{
    public class PlayerView:MonoBehaviour
    {
        [SerializeField] private Transform modelTransform;
        [SerializeField] private GameObject modelView;
        [SerializeField] private ParticleSystem dieParticle;
        [SerializeField] private List<ParticleSystem> trailParticles;

        private Vector3 currentRotation;
        public void SetTilt(float angle)
        {
            Vector3 targetRotation = new Vector3(0,0,-angle);
            if (currentRotation == targetRotation) return;
            currentRotation = targetRotation;
            
            modelTransform.DOLocalRotate(currentRotation, .3f).SetEase(Ease.OutBack);
        }

        public void ResetTilt()
        {
            if(currentRotation == Vector3.zero) return;
            currentRotation = Vector3.zero;
            modelTransform.DOLocalRotate(currentRotation, .3f).SetEase(Ease.OutBack);
        }

        public void SetFall(float angle)
        {
            Vector3 targetRotation = currentRotation;
            targetRotation.x = angle;
            modelTransform.DOLocalRotate(targetRotation, .5f);
        }

        private void ResetRotation()
        {
            modelTransform.localRotation = Quaternion.identity;
        }

        public void Reset()
        {
            ResetRotation();
            modelView.SetActive(true);
        }

        public void OnPlayerDie()
        {
            dieParticle.Play();
            modelView.SetActive(false);
        }

        public void TrailPlay(bool isPlaying)
        {
            switch (isPlaying)
            {
                case true:
                    trailParticles.ForEach(x => x.Play());
                    break;
                default:
                    trailParticles.ForEach(x => x.Stop());
                    break;
            }
        }
    }
}