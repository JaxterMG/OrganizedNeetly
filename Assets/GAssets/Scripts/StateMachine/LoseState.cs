using Core.Controllers;
using UI;
using UnityEngine;

namespace Core.StateMachine.Game
{
    public class LoseState : State
    {
        private LoseScreen _loseScreen;
        public LoseState(GameController gameController, bool isAdditiveState = false) : base(gameController, isAdditiveState)
        {

        }

        public override void Update()
        {
            //Debug.Log($"Gamestate update");
        }

        public void EndGame()
        {
        }

        public override void LoadContent()
        {
            _loseScreen = GameObject.FindAnyObjectByType<LoseScreen>();
            _loseScreen.LoadContent();
            _loseScreen.OnStart();

        }
        public override void OnExit(bool isHide = true)
        {            
            _loseScreen.OnExit();
        }

        public override void OnStart()
        {
        }
    }
    
}