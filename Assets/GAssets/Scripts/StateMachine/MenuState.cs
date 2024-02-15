using Core.Controllers;
using Core.StateMachine.Game;
using Core.StateMachine.Loading;
using UnityEngine;

namespace Core.StateMachine.Menu
{
    public class MenuState : State
    {
        private MenuScreen _menuScreen;
        private SaveLoadHandler _saveLoadHandler;
        public MenuState(EventBus eventBus, GameController gameController, bool isAdditiveState = false) : base(eventBus, gameController, isAdditiveState)
        {
            _menuScreen = GameObject.FindAnyObjectByType<MenuScreen>();
        }
        public override void LoadContent()
        {
            _menuScreen.LoadContent();
            _saveLoadHandler = GameObject.FindObjectOfType<SaveLoadHandler>();
            
        }

        public override void OnStart()
        {
            _menuScreen.MainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            _menuScreen.ShopButton.onClick.AddListener(OnShopButtonButtonClicked);
            _menuScreen.ContinueButton.onClick.AddListener(OnContinueButtonClicked);
            _menuScreen.RestartButton.onClick.AddListener(OnRestartButtonClicked);
            _menuScreen.SettingsButton.onClick.AddListener(OnSettingsButtonClicked);

            _menuScreen.OnStart();
        }


        public override void Update()
        {
        }

        private void OnMainMenuButtonClicked()
        {
            _saveLoadHandler.SaveAll();
            _gameController.ChangeState(new MainMenuState(_eventBus, _gameController));
            _gameController.ExitAdditiveState(this);
        }
        private void OnShopButtonButtonClicked()
        {
            _gameController.CreateAdditiveState(new ShopState(_eventBus, _gameController, true));
        }
        private void OnContinueButtonClicked()
        {
            _gameController.ExitAdditiveState(this);
        }
        private void OnRestartButtonClicked()
        {
            _saveLoadHandler.DeleteSaveFile();
            _gameController.ExitAdditiveState(this);
            _gameController.ChangeState(new GameState(_eventBus, _gameController));
        }
        private void OnSettingsButtonClicked()
        {
            _gameController.CreateAdditiveState(new SettingsState(_eventBus, _gameController, true));
        }

        public override void OnExit(bool isHide = true)
        {
            _menuScreen.OnExit(isHide);
        }
    }
}
