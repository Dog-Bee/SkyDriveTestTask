using System.Collections.Generic;
using Core.Game;
using Signals;
using Zenject;

namespace Infrastructure.Zenject
{
    public class GameStateChangedDispatcher : IInitializable
    {
        private readonly List<IGameStateChanged> _listeners;
        private readonly SignalBus _signalBus;

        public GameStateChangedDispatcher(List<IGameStateChanged> listeners, SignalBus signalBus)
        {
            _listeners = listeners;
            _signalBus = signalBus;
        }
        public void Initialize()
        {
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            _listeners.ForEach(l => l.OnGameStateChanged(signal));
        }
    }
}