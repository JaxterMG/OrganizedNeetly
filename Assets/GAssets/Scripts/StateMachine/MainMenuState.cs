using Core.Controllers;
using UnityEngine;

namespace Core.StateMachine.Menu
{
    public class MainMenuState : State
    {
        private MainMenuScreen _mainMenuScreen;
        public MainMenuState(GameController gameController) : base(gameController)
        {
            _mainMenuScreen = GameObject.FindAnyObjectByType<MainMenuScreen>();
        }
        public override void LoadContent()
        {
            _mainMenuScreen.LoadContent();
        }

        public override void OnStart()
        {
            _mainMenuScreen.OnStart();
        }

        
        public override void Update()
        {
        }

        public override void OnExit()
        {
            _mainMenuScreen.OnExit(true);
        }
    }
}
