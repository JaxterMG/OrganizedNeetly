using Core.Controllers;
using Core.EventBus;

namespace Core.StateMachine
{
    public abstract class State
    {
        protected EventBus.EventBus _eventBus;
        protected bool _isAdditiveState;
        protected GameController _gameController;
        public State(EventBus.EventBus eventBus, GameController gameController, bool isAdditiveState = false)
        {
            _eventBus = eventBus;
            _gameController = gameController;
            _isAdditiveState = isAdditiveState;
        }
        public abstract void OnStart();
        public abstract void Update();
        public abstract void LoadContent();
        public abstract void OnExit(bool isHide = true);
    }
}