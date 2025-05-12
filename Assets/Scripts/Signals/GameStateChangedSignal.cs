namespace Signals
{
    public struct GameStateChangedSignal
    {
        public GameStateType State;
    }

    public enum GameStateType
    {
        Menu,
        Playing,
        GameOver
    }
}