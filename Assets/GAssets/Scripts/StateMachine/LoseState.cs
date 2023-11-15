using Core.Controllers;
using Core.StateMachine.Loading;
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
            _loseScreen = GameObject.FindAnyObjectByType<LoseScreen>();
        }
        public override void LoadContent()
        {
            _loseScreen.LoadContent();
        }

        public override void OnStart()
        {
            _loseScreen.MainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            _loseScreen.ShopButton.onClick.AddListener(OnShopButtonButtonClicked);
            _loseScreen.ContinueButton.onClick.AddListener(OnContinueButtonClicked);
            _loseScreen.RestartButton.onClick.AddListener(OnRestartButtonClicked);

            _loseScreen.OnStart();
        }


        public override void Update()
        {
        }

        private void OnMainMenuButtonClicked()
        {
            _gameController.ChangeState(new MainMenuState(_gameController));
            _gameController.ExitAdditiveState(this);
        }
        private void OnShopButtonButtonClicked()
        {
            _gameController.CreateAdditiveState(new ShopState(_gameController, true));
        }
        private void OnContinueButtonClicked()
        {
            _gameController.ExitAdditiveState(this);
        }
        private void OnRestartButtonClicked()
        {
            _gameController.ExitAdditiveState(this);
            _gameController.ChangeState(new GameState(_gameController));
        }

        public override void OnExit(bool isHide = true)
        {
            _loseScreen.OnExit(isHide);
        }
    }
    
}