using Signals;
using UnityEngine;
using Zenject;

namespace Infrastructure.Input
{
    public class StartGameInputHandler:MonoBehaviour
    {
        private SignalBus _signalBus;
        private IInputService _inputService;
        private bool _hasStarted = false;

        [Inject] private void Construct(SignalBus signalBus, IInputService inputService)
        {
            _signalBus = signalBus;
            _inputService = inputService;
            
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void Update()
        {
            if (_hasStarted) return;

            if (_inputService.IsAnyKeyPressed())
            {
                _signalBus.Fire(new GameStartedSignal());
            }
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            _hasStarted = signal.State == GameStateType.Playing || signal.State == GameStateType.GameOver;
        }
    }
}