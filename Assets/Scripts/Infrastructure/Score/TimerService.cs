using System.Collections;
using System.Collections.Generic;
using Signals;
using UnityEngine;
using Zenject;

public class TimerService : IInitializable,ITickable
{
    private readonly SignalBus _signalBus;
    
    private float _elapsedTime;
    private bool _isActive;
    
    public float ElapsedTime=>_elapsedTime;

    public TimerService(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    public void Initialize()
    {
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
    }

    public void Tick()
    {
        if(_isActive)
            _elapsedTime += Time.deltaTime;
    }

    private void OnGameStateChanged(GameStateChangedSignal signal)
    {
        _isActive = signal.State == GameStateType.Playing;
        
        if(signal.State == GameStateType.Menu)
            Reset();
    }

    private void Reset()
    {
        _elapsedTime = 0;
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(_elapsedTime / 60);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60);
        
        return $"{minutes}:{seconds}";
    }
}
