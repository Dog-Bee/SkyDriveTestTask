using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
   [SerializeField] private ObstacleSpawnerConfig config;
   
   private List<GameObject> _obstaclePrefabs;
   private int _gridWidth;
   private float _spacing;
   private float _zOffsetSpacing;

   private System.Random _rnd = new();

   private void Start()
   {
      _obstaclePrefabs = config.ObstaclePrefabs;
      _gridWidth = config.GridWidth;
      _spacing = config.Spacing;
      _zOffsetSpacing = config.ZOffsetSpacing;
   }

   public void SpawnObstacles(Transform parent, float zOffset, DifficultyService difficultyService)
   {
      float spawnChance = difficultyService.GetSpawnChance();

      Vector2Int[] path = GeneratePath(_gridWidth, 3);
      
      for(int x=0; x<_gridWidth;x++)     
      {
         if (path[0].x == x) continue;
         double rndChance = _rnd.NextDouble();
         if(rndChance>spawnChance)continue;
         
         var prefab = _obstaclePrefabs[_rnd.Next(_obstaclePrefabs.Count)];
         Vector3 pos = new Vector3((x - _gridWidth / 2) * _spacing, 1f, Random.Range(zOffset-_zOffsetSpacing, zOffset+_zOffsetSpacing));
         Instantiate(prefab, pos, Quaternion.identity,parent);
      }
   }

   private Vector2Int[] GeneratePath(int width, int depth)
   {
      List<Vector2Int> path = new();
      int currentX = _rnd.Next(width);

      for (int z = 0; z < depth; z++)
      {
         int direction = Random.Range(-1, 2);
         currentX = Mathf.Clamp(currentX+direction, 0, width - 1);
         path.Add(new Vector2Int(currentX, z));
      }
      return path.ToArray();
   }
}
