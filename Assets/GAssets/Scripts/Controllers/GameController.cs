using Core.StateMachine;
using Core.StateMachine.Game;
using UnityEngine;
using Core.Localization;
using Core.StateMachine.Loading;
using System.Collections.Generic;
using Zenject;
using UnityEngine.SocialPlatforms;

namespace Core.Controllers
{
    public class GameController : MonoBehaviour
    {
        [Inject] public IScoreController ScoreController;
        [Inject] EventBus _eventBus;
        private State _currentState;
        private List<State> _additiveStates = new List<State>();

        void Awake()
        {
            _currentState = new LoadingState(_eventBus, this);
            LoadContent();
        }

        private void LoadContent()
        {
            CSVTranslationsReader.InitializeTranslations("Localization/Localization");
            ChangeState(new LoadingState(_eventBus, this));
        }

        private void Update()
        {
            _currentState.Update();
            for (int i = _additiveStates.Count - 1; i >= 0; i--)
            {
                var additiveState = _additiveStates[i];
                additiveState.Update();
            }
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
