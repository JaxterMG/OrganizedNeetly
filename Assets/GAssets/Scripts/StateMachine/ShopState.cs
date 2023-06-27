using Core.Controllers;
using UnityEngine;

namespace Core.StateMachine.Loading
{
    public class ShopState : State
    {
        private ShopScreen _shopScreen;
        public ShopState(GameController gameController) : base(gameController)
        {
            _shopScreen = GameObject.FindAnyObjectByType<ShopScreen>();
        }
        public override void LoadContent()
        {
            _shopScreen.LoadContent();
        }

        public override void OnStart()
        {
            _shopScreen.OnStart();
        }

        
        public override void Update()
        {
        }

        public override void OnExit()
        {
            _shopScreen.OnExit();
        }

        
    }
}
