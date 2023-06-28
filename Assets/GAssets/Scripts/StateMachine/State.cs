using Core.Controllers;

namespace Core.StateMachine
{
    public abstract class State
    {
        protected GameController _gameController;
        public State(GameController gameController)
        {
            _gameController = gameController;
        }
        public abstract void OnStart();
        public abstract void Update();
        public abstract void LoadContent();
        public abstract void OnExit(bool isHide = true);
    }
}