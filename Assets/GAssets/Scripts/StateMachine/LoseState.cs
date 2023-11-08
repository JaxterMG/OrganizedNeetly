using Core.Controllers;
using Core.StateMachine.Menu;
using UI;
using UnityEngine;

namespace Core.StateMachine.Game
{
    public class LoseState : State
    {
        private LoseScreen _loseScreen;
        public LoseState(GameController gameController, bool isAdditiveState = false) : base(gameController, isAdditiveState)
        {
        }
        public override void OnStart()
        {
            //_loseScreen.PauseButton.onClick.AddListener(OnPauseButtonPressed);
            _loseScreen.OnStart();
        }

        public override void Update()
        {
            //Debug.Log($"Gamestate update");
        }

        public override void LoadContent()
        {
            _loseScreen = GameObject.FindAnyObjectByType<LoseScreen>();
            _loseScreen.LoadContent();

        }
        private void OnPauseButtonPressed()
        {
            _gameController.CreateAdditiveState(new MenuState(_gameController, true));
        }
        public void OnGameFail()
        {
            _gameController.CreateAdditiveState(new LoseState(_gameController, true));
        }
        public override void OnExit(bool isHide = true)
        {
            _loseScreen.OnExit();
        }
    }
    
}