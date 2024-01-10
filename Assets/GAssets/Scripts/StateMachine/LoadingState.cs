using System.Threading.Tasks;
using Core.Controllers;
using Core.StateMachine.Menu;
using UnityEngine;

namespace Core.StateMachine.Loading
{
    public class LoadingState : State
    {
        private LoadingScreen _loadingScreen;
        public LoadingState(EventBus eventBus, GameController gameController, bool isAdditiveState = false) : base(eventBus, gameController, isAdditiveState)
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
            _gameController.ChangeState(new MainMenuState(_eventBus, _gameController));
        }

        
        public override void Update()
        {
        }

        public override void OnExit(bool isHide = true)
        {
            _loadingScreen.OnExit(isHide);
        }

        
    }
}
