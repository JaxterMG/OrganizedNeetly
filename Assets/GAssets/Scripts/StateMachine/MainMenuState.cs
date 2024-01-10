using Core.Controllers;
using Core.StateMachine.Game;
using Core.StateMachine.Loading;
using UnityEngine;

namespace Core.StateMachine.Menu
{
    public class MainMenuState : State
    {
        private MainMenuScreen _mainMenuScreen;
        public MainMenuState(EventBus eventBus, GameController gameController, bool isAdditiveState = false) : base(eventBus, gameController, isAdditiveState)
        {
            _mainMenuScreen = GameObject.FindAnyObjectByType<MainMenuScreen>();
        }
        public override void LoadContent()
        {
            _mainMenuScreen.LoadContent();
        }

        public override void OnStart()
        {
            _mainMenuScreen.PlayButton.onClick.AddListener(OnPlayButtonPressed);
            _mainMenuScreen.ShopButton.onClick.AddListener(OnShopButtonPressed);
            _mainMenuScreen.LikeButton.onClick.AddListener(OnLikeButtonPressed);
            _mainMenuScreen.SettingsButton.onClick.AddListener(OnSettingsButtonPressed);

            _mainMenuScreen.OnStart();
        }


        public override void Update()
        {
        }

        private void OnPlayButtonPressed()
        {
            _gameController.ChangeState(new GameState(_eventBus, _gameController));
        }
        private void OnShopButtonPressed()
        {
            _gameController.ChangeState(new ShopState(_eventBus, _gameController), false);
        }
        private void OnLikeButtonPressed()
        {
            //_gameController.ChangeState(new GameState(_gameController));
        }
        private void OnSettingsButtonPressed()
        {
            _gameController.ChangeState(new SettingsState(_eventBus, _gameController), false);
        }

        public override void OnExit(bool isHide = true)
        {
            _mainMenuScreen.OnExit(isHide);
        }
    }
}
