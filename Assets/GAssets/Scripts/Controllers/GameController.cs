using Core.StateMachine;
using Core.StateMachine.Game;
using Core.StateMachine.Menu;
using UnityEngine;
using Core.Localization;

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
            CSVTranslationsReader.InitializeTranslations("Localization/Localization");
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
