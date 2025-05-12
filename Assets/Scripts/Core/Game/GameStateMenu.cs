using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Core.Game
{
    public class GameStateMenu:IGameState
    {
        public void Enter()
        {
            Debug.Log("Enter Menu, await for input");
        }

        public void Exit()
        {
            Debug.Log("Exit Menu");
        }
    }
}