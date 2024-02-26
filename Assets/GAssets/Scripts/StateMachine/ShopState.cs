using Core.Controllers;
using Core.StateMachine.Menu;
using UnityEngine;

namespace Core.StateMachine.Loading
{
    public class ShopState : State
    {
        private ShopScreen _shopScreen;
        public ShopState(EventBus eventBus, GameController gameController, bool isAdditiveState = false) : base(eventBus, gameController, isAdditiveState)
        {
            _shopScreen = GameObject.FindAnyObjectByType<ShopScreen>();
        }
        public override void LoadContent()
        {
            _shopScreen.LoadContent();
        }

        public override void OnStart()
        {
            for (int i = 0; i < _shopScreen.ShopItems.Length; i++)
            {
                _shopScreen.ShopItems[i].ThemeChanged += OnBackButtonPressed;
            }
            
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
            _gameController.ChangeState(new MainMenuState(_eventBus, _gameController), false);
        }

        public override void OnExit(bool isHide = true)
        {
            for (int i = 0; i < _shopScreen.ShopItems.Length; i++)
            {
                _shopScreen.ShopItems[i].ThemeChanged -= OnBackButtonPressed;
            }
            _shopScreen.OnExit();
        }

        
    }
}
