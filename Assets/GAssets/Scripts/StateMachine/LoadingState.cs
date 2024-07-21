using System.Threading.Tasks;
using Core.Controllers;
using Core.Save;
using Core.UI;
using UnityEngine;
using Core.EventBus;

namespace Core.StateMachine
{
    public class LoadingState : State
    {
        private LoadingScreen _loadingScreen;
        public LoadingState(EventBus.EventBus eventBus, GameController gameController, bool isAdditiveState = false) : base(eventBus, gameController, isAdditiveState)
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
            if(!GameObject.FindObjectOfType<SaveLoadHandler>().HasSaveFile())
                _gameController.ChangeState(new MainMenuState(_eventBus, _gameController));
            else
            {
                _gameController.ChangeState(new GameState(_eventBus, _gameController));
            }
            
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
