using Core.Player;
using Signals;
using Unity.VisualScripting;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace Core.Game
{
    public class GameManager : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly GameStateMenu _menu;
        private readonly GameStatePlaying _playing;
        private readonly GameStateGameOver _gameOver;
        

        private IGameState _currentState;

        public GameManager(SignalBus signalBus, GameStateMenu menu, GameStatePlaying playing,
            GameStateGameOver gameOver)
        {
            _signalBus = signalBus;
            _menu = menu;
            _playing = playing;
            _gameOver = gameOver;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);

            EnterState(_menu, GameStateType.Menu);
        }

        private void OnGameStarted() => EnterState(_playing, GameStateType.Playing);
        private void OnPlayerDied() => EnterState(_gameOver, GameStateType.GameOver);

        private void EnterState(IGameState newState, GameStateType stateType)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
            
            _signalBus.Fire(new GameStateChangedSignal { State = stateType });
        }
    }
}