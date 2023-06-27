using Core.Controllers;
using UnityEngine;

namespace Core.StateMachine.Loading
{
    public class LoadingState : State
    {
        private LoadingScreen _loadingScreen;
        public LoadingState(GameController gameController) : base(gameController)
        {
            _loadingScreen = GameObject.FindAnyObjectByType<LoadingScreen>();
        }
        public override void LoadContent()
        {
            _loadingScreen.LoadContent();
        }

        public override void OnStart()
        {
            _loadingScreen.OnStart();
        }

        
        public override void Update()
        {
        }

        public override void OnExit()
        {
            _loadingScreen.OnExit();
        }

        
    }
}
