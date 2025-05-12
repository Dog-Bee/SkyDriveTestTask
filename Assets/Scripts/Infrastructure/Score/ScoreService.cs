using System;
using Signals;
using UnityEngine;
using Zenject;

namespace Infrastructure.Score
{
    public class ScoreService:IInitializable,IDisposable,ITickable
    {
        private readonly SignalBus _signalBus;
        
        private const String HIGH_SCORE_KEY = "HighScore";
        private const int TIMER_INTERVAL = 1;

        private float _subTimer;
        
        private float _score=0;
        private float _highScore=0;
        private int _asteroidCount=0;
        
        private bool _isBoosting=false;
        private bool _isActive;
        
        
        public float Score => _score;
        public float HighScore => _highScore;
        public int AsteroidCount => _asteroidCount;
        
        public ScoreService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        public void Initialize()
        {
            _highScore = PlayerPrefs.GetFloat(HIGH_SCORE_KEY,0);
            _signalBus.Subscribe<AsteroidPassedSignal>(OnAsteroidPassed);
            _signalBus.Subscribe<BoostStateChangedSignal>(OnBoostChanged);
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }
        public void Tick()
        {
            if (!_isActive) return;
            _subTimer += Time.deltaTime;
            if (_subTimer >= TIMER_INTERVAL)
            {
                _subTimer = 0;
                AddScore(_isBoosting?2:1);
            }
        }
        private void OnBoostChanged(BoostStateChangedSignal signal)=>_isBoosting=signal.IsBoosting;

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            _isActive = signal.State == GameStateType.Playing;
            if (signal.State == GameStateType.Menu)
            {
                ResetScore();
            }
        }

        public void AddScore(float amount)
        {
            _score += amount;

            if (_score > _highScore)
            {
                _highScore = _score;
                PlayerPrefs.SetFloat(HIGH_SCORE_KEY,_highScore);
                PlayerPrefs.Save();
            }
            
            _signalBus.Fire(new ScoreChangedSignal
            {
                Score = _score,
                HighScore = _highScore,
                AsteroidCount = _asteroidCount
            });
        }

        public void ResetScore()
        {
            _score = 0;
            _asteroidCount = 0;
            _signalBus.Fire(new ScoreChangedSignal
            {
                Score = _score,
                HighScore = _highScore,
                AsteroidCount = _asteroidCount
            });
        }

        private void OnAsteroidPassed(AsteroidPassedSignal signal)
        {
            _asteroidCount++;
            AddScore(5);
        }
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<AsteroidPassedSignal>(OnAsteroidPassed);
        }

        
    }
}