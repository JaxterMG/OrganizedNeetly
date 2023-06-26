using System.Diagnostics;
using Core.Controllers;
using UnityEngine;

namespace Core.StateMachine
{
    public abstract class State
    {
        protected GameController _gameController;
        public State(GameController gameController)
        {
            _gameController = gameController;
        }
        public abstract void Update();
        public abstract void LoadContent();
        public abstract void OnExit();
    }
}