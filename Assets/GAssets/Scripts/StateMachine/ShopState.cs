using Core.Controllers;
using Core.StateMachine.Menu;
using UnityEngine;

namespace Core.StateMachine.Loading
{
    public class ShopState : State
    {
        private ShopScreen _shopScreen;
        public ShopState(GameController gameController, bool isAdditiveState = false) : base(gameController, isAdditiveState)
        {
            _shopScreen = GameObject.FindAnyObjectByType<ShopScreen>();
        }
        public override void LoadContent()
        {
            _shopScreen.LoadContent();
        }

        public override void OnStart()
        {
            _shopScreen.BackButton.onClick.AddListener(OnBackButtonPressed);
            _shopScreen.OnStart();
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
            _gameController.ChangeState(new MainMenuState(_gameController), false);
        }

        public override void OnExit(bool isHide = true)
        {
            _shopScreen.OnExit();
        }

        
    }
}
