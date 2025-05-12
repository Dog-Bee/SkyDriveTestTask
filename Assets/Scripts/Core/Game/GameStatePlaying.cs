using UnityEngine;

namespace Core.Game
{
    public class GameStatePlaying:IGameState
    {
        public void Enter()
        {
            Debug.Log("Game State Playing");
        }

        public void Exit()
        {
            Debug.Log("Exiting Playing Game State");
        }
    }
}