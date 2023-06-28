using System.Threading.Tasks;
using Core.Controllers;
using Core.StateMachine.Menu;
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

        public override async void OnStart()
        {
            _loadingScreen.OnStart();
            await Task.Delay(5000);
            _gameController.ChangeState(new MainMenuState(_gameController));
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
