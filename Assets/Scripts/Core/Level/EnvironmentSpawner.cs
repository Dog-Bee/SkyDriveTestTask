using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Level
{
    public class EnvironmentSpawner:MonoBehaviour
    {
        [SerializeField] private EnvironmentSpawnerConfig config;

       
        public void TrySpawnEnvironment(Transform parent)
        {
            int count = Random.Range(0, config.MaxEnvironmentPerPlatform+1);
            List<Vector3> usedPositions = new();

            for (int i = 0; i < count; i++)
            {
                Vector3 position = GetNonOverlappingPosition(usedPositions,parent.position.z);
                
                if (position == Vector3.zero) continue;
                
                GameObject env = config.EnvironmentPrefabs[Random.Range(0, config.EnvironmentPrefabs.Count)];
                Instantiate(env, position, Quaternion.identity, parent);
                usedPositions.Add(position);
            }
        }

        private Vector3 GetNonOverlappingPosition(List<Vector3> usedPositions,float zOffset)
        {
            for (int i = 0; i < config.AttemptsFindSpot; i++)
            {
                Vector3 candidate = PlanetPosition(zOffset);
                bool isOverlapping = usedPositions.Exists(p=> Vector3.Distance(p, candidate) < config.MinDistanceBetweenEnvironments);
                if(!isOverlapping)
                    return candidate;
            }
            
            return Vector3.zero;
        }

        private Vector3 PlanetPosition(float zOffset)
        {
            int zone = Random.Range(0, 2); // 0-LeftZone 1-RightZone
            float x = 0;
            float y = 0;
            float z = 0;

            switch (zone)
            {
               case 0:
                   x = Random.Range(config.LeftZone.x, config.LeftZone.y);
                   y = Random.Range(config.HeightRange.x, config.HeightRange.y);
                   break;
               case 1:
                   x = Random.Range(config.RightZone.x, config.RightZone.y);
                   y = Random.Range(config.HeightRange.x, config.HeightRange.y);
                   break;
            }
            z =zOffset+Random.Range(config.ForwardRange.x, config.ForwardRange.y);
            return new Vector3(x,y,z);
        }
    }
}