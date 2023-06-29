using Core.StateMachine;
using Core.StateMachine.Game;
using UnityEngine;
using Core.Localization;
using Core.StateMachine.Loading;

namespace Core.Controllers
{
    public class GameController : MonoBehaviour
    {
        private State _currentState;
        private State _additiveState;

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
        public void ChangeState(State state, bool isHidePrevious = true)
        {
            _currentState.OnExit(isHidePrevious);
            _currentState = state;
            _currentState.LoadContent();
            _currentState.OnStart();
        }
        public void CreateAdditiveState(State state, bool isHidePrevious = true)
        {
            _additiveState?.OnExit(isHidePrevious);

            _additiveState = state;
            _additiveState.LoadContent();
            _additiveState.OnStart();
        }
    }
}
