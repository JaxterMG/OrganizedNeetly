using System.Data;
using System.Threading.Tasks;
using Core.Controllers;
using Core.StateMachine.Loading;
using Core.StateMachine.Menu;
using UI;
using UnityEngine;

namespace Core.StateMachine.Game
{
    public class ReviveState : State
    {
        private IScoreController _scoreController;
        private ReviveScreen _reviveScreen;
        private float _reviveOptionTime = 5;
        public ReviveState(EventBus eventBus, GameController gameController, bool isAdditiveState = false) : base(eventBus, gameController, isAdditiveState)
        {
            _reviveScreen = GameObject.FindAnyObjectByType<ReviveScreen>();
        }
        public override void LoadContent()
        {
            _reviveScreen.LoadContent();
        }

        public override async void OnStart()
        {
            _eventBus.Publish<string>(EventType.PlaySound, "Lose");

            _reviveScreen.ReviveButton.onClick.AddListener(OnReviveButtonClicked);
            _reviveScreen.DeclineButton.onClick.AddListener(OnDeclineButtonClicked);
            _reviveScreen.OnStart();
            await Task.Delay(400);
            if(_scoreController.IsHighScore())
            {
                //_eventBus.Publish<string>(EventType.PlaySound, "HighScore");
            }
        }

        public void LinkScoreController(IScoreController scoreController)
        {
            _scoreController = scoreController;
        }


        public override void Update()
        {
            if(_reviveScreen.Radial.fillAmount <= 0) return;

            _reviveScreen.Radial.fillAmount -= Time.deltaTime / _reviveOptionTime;
            if(_reviveScreen.Radial.fillAmount <= 0)
            {
                OnDeclineButtonClicked();
            }
        }

        private void OnReviveButtonClicked()
        {   
            _eventBus.Publish<int>(EventType.Revive, 3);            
            _gameController.ExitAdditiveState(this);
        }
        private void OnDeclineButtonClicked()
        {
            LoseState loseState = new LoseState(_eventBus, _gameController, true);
            loseState.LinkScoreController(_scoreController);
            _gameController.CreateAdditiveState(loseState);
            _gameController.ExitAdditiveState(this);
        }

        public override void OnExit(bool isHide = true)
        {
            _reviveScreen.OnExit(isHide);
        }
    }
    
}