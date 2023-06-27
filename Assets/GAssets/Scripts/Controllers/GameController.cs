using Core.StateMachine;
using Core.StateMachine.Game;
using Core.StateMachine.Menu;
using UnityEngine;
using Core.Localization;
using Core.StateMachine.Loading;

namespace Core.Controllers
{
    public class GameController : MonoBehaviour
    {
        private State _currentState;
        private State _previousState;

        void Awake()
        {
            _currentState = new LoadingState(this);
            LoadContent();
        }

        private void LoadContent()
        {
            CSVTranslationsReader.InitializeTranslations("Localization/Localization");
            ChangeState(new LoadingState(this));
        }

        private void Update()
        {
            _currentState.Update();
        }
        public void ChangeState(State state)
        {
            _previousState = _currentState;
            _currentState.OnExit();
            _currentState = state;
            _currentState.LoadContent();
            _currentState.OnStart();
        }
    }
}
