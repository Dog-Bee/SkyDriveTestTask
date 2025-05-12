using Signals;

namespace Core.Game
{
    public interface IGameStateChanged
    {
        public void OnGameStateChanged(GameStateChangedSignal signal);
    }
}