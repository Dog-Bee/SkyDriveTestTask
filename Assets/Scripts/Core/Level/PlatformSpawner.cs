using System;
using System.Collections;
using System.Collections.Generic;
using Core.Level;
using Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Transform playerTransform;
    
    private List<PlatformSegment> _platformPrefabs;
    private PlatformSegment _wholePlatform;
    private int _initialSegments = 10;
    private int _safeSegments = 3;
    private float _segmentLength = 10;
    private float _spawnDistance = 60f;

    private readonly Queue<GameObject> _activePlatforms = new();
    private ObstacleSpawner _obstacleSpawner;
    private EnvironmentSpawner _environmentSpawner;
    private DifficultyService _difficultyService;
    private float _currentZ;
    private bool _lastNarrow = true;

    [Inject] private void Construct(ObstacleSpawner obstacleSpawner,EnvironmentSpawner environmentSpawner, DifficultyService difficultyService,PlatformSpawnerConfig config,
        SignalBus signalBus)
    {
        _obstacleSpawner = obstacleSpawner;
        _difficultyService = difficultyService;
        _environmentSpawner = environmentSpawner;
        
        _platformPrefabs = config.PlatformPrefab;
        _wholePlatform = config.WholePlatform;
        _initialSegments = config.InitialSegments;
        _safeSegments = config.SafeSegments;
        _segmentLength = config.SegmentLength;
        _spawnDistance = config.SpawnDistance;
        
        signalBus.Subscribe<GameStateChangedSignal>(OnGameStarted);
    }

    private void Update()
    {
        if(_activePlatforms.Count == 0) return;
        if (playerTransform.position.z + _spawnDistance > _currentZ)
        {
            SpawnSegments(false);
            RemoveOldest();
        }
    }

    private void OnGameStarted(GameStateChangedSignal signal)
    {
        if (signal.State != GameStateType.Menu) return;
        Reset();
        
        for (int i = 0; i < _initialSegments; i++)
        {
            SpawnSegments(i<_safeSegments);
        }
    }

    private void SpawnSegments(bool isSafe)
    {
        PlatformSegment segment = Instantiate(isSafe || _lastNarrow?_wholePlatform:_platformPrefabs[Random.Range(0,_platformPrefabs.Count)],
            new Vector3(0,0,_currentZ), Quaternion.identity, parent);
        _lastNarrow = segment.IsNarrow;
        if(!isSafe)
        isSafe = segment.IsSafe || segment.IsNarrow;
        
        float rnd = Random.value;
        
        if (!isSafe && rnd > _difficultyService.GetEmptyPlatformChance())
        {
            _obstacleSpawner.SpawnObstacles(segment.transform,_currentZ,_difficultyService);
        }
        _environmentSpawner.TrySpawnEnvironment(segment.transform);
        
        _activePlatforms.Enqueue(segment.gameObject);
        _currentZ += _segmentLength;
    }

    private void RemoveOldest()
    {
        var segment = _activePlatforms.Dequeue();
        Destroy(segment);
    }

    private void Reset()
    {
        _lastNarrow = true;
        if(_activePlatforms.Count == 0) return;

        foreach (var platform in _activePlatforms)
        {
            Destroy(platform.gameObject);
        }
        _activePlatforms.Clear();
        _currentZ = 0;
    }
}
