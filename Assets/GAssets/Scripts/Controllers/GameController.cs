using Core.StateMachine;
using Core.StateMachine.Game;
using UnityEngine;
using Core.Localization;
using Core.StateMachine.Loading;
using System.Collections.Generic;

namespace Core.Controllers
{
    public class GameController : MonoBehaviour
    {
        private State _currentState;
        private List<State> _additiveStates = new List<State>();

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
        public void CreateAdditiveState(State additiveState)
        {
            _additiveStates.Add(additiveState);
            additiveState.LoadContent();
            additiveState.OnStart();
        }

        public void ExitAdditiveState(State additiveState, bool isHidePrevious = true)
        {
            _additiveStates.Remove(additiveState);
            additiveState.OnExit(isHidePrevious);
        }
    }
}
