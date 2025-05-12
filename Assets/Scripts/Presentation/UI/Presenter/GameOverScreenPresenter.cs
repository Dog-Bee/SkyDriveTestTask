using System;
using Infrastructure.Score;
using Signals;
using Zenject;

public class GameOverScreenPresenter : IInitializable, IDisposable
{
    private readonly SignalBus _signalBus;
    private readonly GameOverScreenView _view;
    private readonly ScoreService _scoreService;
    private readonly TimerService _timerService;

    public GameOverScreenPresenter(SignalBus signalBus, GameOverScreenView view, ScoreService scoreService,
        TimerService timerService)
    {
        _signalBus = signalBus;
        _view = view;
        _scoreService = scoreService;
        _timerService = timerService;
    }
    public void Initialize()
    {
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChange);
        _view.Restart(Restart);
    }

    public void Dispose()
    {
        _signalBus.TryUnsubscribe<GameStateChangedSignal>(OnGameStateChange);
    }

    private void OnGameStateChange(GameStateChangedSignal signal)
    {
        if (signal.State != GameStateType.GameOver) return;
        bool isNewHighScore = _scoreService.Score == _scoreService.HighScore;
        _view.StatUpdate(_scoreService.Score,_scoreService.AsteroidCount,_timerService.GetFormattedTime(),isNewHighScore);
    }

    private void Restart()
    {
        _signalBus.Fire(new GameStateChangedSignal{ State = GameStateType.Menu});
    }
    
}
