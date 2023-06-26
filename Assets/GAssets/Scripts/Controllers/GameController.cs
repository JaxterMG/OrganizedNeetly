using Core.StateMachine;
using Core.StateMachine.Game;
using Core.StateMachine.Menu;
using UnityEngine;

namespace Core.Controllers
{
    public class GameController : MonoBehaviour
    {
        private State _currentState;
        void Awake()
        {
            LoadContent();
        }

        private void LoadContent()
        {
            _currentState = new GameState(this);
            _currentState.LoadContent();
        }

        private void Update()
        {
            _currentState.Update();
        }
        public void ChangeState(State state)
        {
            _currentState = state;
        }
    }
}
