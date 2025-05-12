using System;
using Signals;
using UnityEngine;
using Zenject;

public class DifficultyService:IInitializable,IDisposable
{
    private readonly AnimationCurve _spawnChanceCurve;
    private readonly AnimationCurve _emptyPlatformCurve;
    private readonly AnimationCurve _speedMultiplierCurve;
    
    private float _currentScore;
   [Inject] private SignalBus _signalBus;

    public DifficultyService(AnimationCurve spawnChanceCurve, AnimationCurve emptyPlatformCurve,AnimationCurve speedMultiplierCurve)
    {
        _spawnChanceCurve = spawnChanceCurve;
        _emptyPlatformCurve = emptyPlatformCurve;
        _speedMultiplierCurve = speedMultiplierCurve;
    }
    
    public void Initialize()
    {
        _signalBus.Subscribe<ScoreChangedSignal>(UpdateScore);
    }

    public void UpdateScore(ScoreChangedSignal signal)
    {
        _currentScore = signal.Score;
    }
    
    public float GetDifficultyValue() => _currentScore;

    public float GetSpawnChance()
    {
        return _spawnChanceCurve.Evaluate(_currentScore);
    }

    public float GetEmptyPlatformChance()
    {
        return _emptyPlatformCurve.Evaluate(_currentScore);
    }

    public float GetSpeedMultiplier()
    {
        return _speedMultiplierCurve.Evaluate(_currentScore);
    }
    
    public void Dispose()
    {
        _signalBus.TryUnsubscribe<ScoreChangedSignal>(UpdateScore);
    }
}
