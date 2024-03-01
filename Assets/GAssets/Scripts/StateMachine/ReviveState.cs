using System.Threading.Tasks;
using Core.Controllers;
using UI;
using UnityEngine;
using Zenject.SpaceFighter;

namespace Core.StateMachine.Game
{
    public class ReviveState : State, ISavable
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
            _eventBus.Publish<string>(BusEventType.PlaySound, "Lose");

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
            PlayerPrefs.SetInt("Revive", 1);
            _eventBus.Publish<int>(BusEventType.Revive, 3);            
            _gameController.ExitAdditiveState(this);
        }
        private void OnDeclineButtonClicked()
        {
            PlayerPrefs.SetInt("Revive", 0);
            LoseState loseState = new LoseState(_eventBus, _gameController, true);
            loseState.LinkScoreController(_scoreController);
            _gameController.CreateAdditiveState(loseState);
            _gameController.ExitAdditiveState(this);
        }

        public override void OnExit(bool isHide = true)
        {
            _reviveScreen.OnExit(isHide);
        }

        private struct ReviveBonusData
        {

        }
        public string Save()
        {
            throw new System.NotImplementedException();
        }

        public void Load(string jsonData)
        {
            throw new System.NotImplementedException();
        }
    }
    
}