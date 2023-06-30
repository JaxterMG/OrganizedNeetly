using Core.Controllers;

namespace Core.StateMachine
{
    public abstract class State
    {
        protected bool _isAdditiveState;
        protected GameController _gameController;
        public State(GameController gameController, bool isAdditiveState = false)
        {
            _gameController = gameController;
            _isAdditiveState = isAdditiveState;
        }
        public abstract void OnStart();
        public abstract void Update();
        public abstract void LoadContent();
        public abstract void OnExit(bool isHide = true);
    }
}