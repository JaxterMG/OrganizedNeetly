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
           // _loadingScreen.CanvasGroup
        }

        public override void LoadContent()
        {
        }

        public override void Update()
        {
        }

        public override void OnExit()
        {
        }
    }
}
