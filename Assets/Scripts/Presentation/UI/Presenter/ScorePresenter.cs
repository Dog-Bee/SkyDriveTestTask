using System;
using Signals;
using UnityEngine;
using Zenject;

namespace Infrastructure.Score
{
    public class ScorePresenter:IInitializable,IDisposable,ITickable
    {
        private readonly ScoreView _scoreView;
        private readonly ScoreService _scoreService;
        private readonly SignalBus _signalBus;
        private readonly TimerService _timerService;

        private int _asteroidPassed;
        private float _timer;

        public ScorePresenter(ScoreView scoreView, ScoreService scoreService,TimerService timerService ,SignalBus signalBus)
        {
            _scoreView = scoreView;
            _scoreService = scoreService;
            _signalBus = signalBus;
            _timerService = timerService;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<ScoreChangedSignal>(OnScoreChanged);
        }

        public void Tick()
        {
            _scoreView.SetFlightTime(_timerService.GetFormattedTime());
        }
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<ScoreChangedSignal>(OnScoreChanged);
        }

        private void OnScoreChanged(ScoreChangedSignal signal)
        {
            _scoreView.SetScore((int)signal.Score, (int)signal.HighScore);
            _scoreView.SetAsteroidPassText(signal.AsteroidCount);
        }

       
    }
}