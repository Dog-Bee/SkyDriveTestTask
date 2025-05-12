using System;
using Core.Player;
using Signals;
using Zenject;
using IInitializable = Unity.VisualScripting.IInitializable;

namespace Infrastructure.Score
{
    public class SpeedometerPresenter:IInitializable,IDisposable,ITickable
    {
        private readonly SpeedometerView _speedometerView;
        private readonly SignalBus _signalBus;
        private readonly PlayerModel _playerModel;
        private readonly DifficultyService _difficultyService;
 
        private bool _isActive;

        public SpeedometerPresenter(SignalBus signalBus, PlayerModel playerModel,SpeedometerView speedometerView,DifficultyService difficultyService)
        {
            _signalBus = signalBus;
            _playerModel = playerModel;
            _speedometerView = speedometerView;
            _difficultyService = difficultyService;
        }
        public void Initialize()
        {
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }
        public void Tick()
        {
            if(_isActive) return;
            _speedometerView.UpdateSpeed((int)_playerModel.CurrentSpeed(_difficultyService.GetSpeedMultiplier()));
        }
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            _isActive = signal.State == GameStateType.Playing;

            switch (_isActive)
            {
                case true:
                    _speedometerView.Show();
                    break;
                case false:
                    _speedometerView.Hide();
                    break;
            }
                
        }
        
        
    }
}