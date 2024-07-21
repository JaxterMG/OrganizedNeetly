using Core.Controllers;
using Core.UI;
using UnityEngine;
using Core.EventBus;

namespace Core.StateMachine
{
    public class SettingsState : State
    {
        private SettingsScreen _settingsScreen;
        public SettingsState(EventBus.EventBus eventBus, GameController gameController, bool isAdditiveState = false) : base(eventBus, gameController, isAdditiveState)
        {
            _settingsScreen = GameObject.FindAnyObjectByType<SettingsScreen>();
        }
        public override void LoadContent()
        {
            _settingsScreen.LoadContent();
        }

        public override void OnStart()
        {
            _settingsScreen.BackButton.onClick.AddListener(OnBackButtonPressed);
            _settingsScreen.OnStart();
        }

        
        public override void Update()
        {
        }

        private void OnBackButtonPressed()
        {
            if(_isAdditiveState)
            {
                _gameController.ExitAdditiveState(this);
                return;
            }
            _gameController.ChangeState(new MainMenuState(_eventBus, _gameController), false);
        }

        public override void OnExit(bool isHide = true)
        {
            _settingsScreen.OnExit();
        }

        
    }
}
