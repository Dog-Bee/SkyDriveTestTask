using UnityEngine;

namespace Core.Game
{
    public class GameStateGameOver:IGameState
    {
        public void Enter()
        {
            Debug.Log("Game Over");
        }

        public void Exit()
        {
            Debug.Log("Exit Game Over");
        }
    }
}