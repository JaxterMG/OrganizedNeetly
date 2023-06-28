using Core.Controllers;
using UnityEngine;

namespace Core.StateMachine.Menu
{
    public class MenuState : State
    {
        private MenuScreen _menuScreen;
        public MenuState(GameController gameController) : base(gameController)
        {
            _menuScreen = GameObject.FindAnyObjectByType<MenuScreen>();
        }
        public override void LoadContent()
        {
            _menuScreen.LoadContent();
        }

        public override void OnStart()
        {
            _menuScreen.OnStart();
        }

        
        public override void Update()
        {
        }

        public override void OnExit()
        {
            _menuScreen.OnExit(true);
        }
    }
}
