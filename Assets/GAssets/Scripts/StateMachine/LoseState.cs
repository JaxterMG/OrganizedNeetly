using Core.Controllers;

namespace Core.StateMachine.Game
{
    public class LoseState : State
    {
        public LoseState(GameController gameController, bool isAdditiveState = false) : base(gameController, isAdditiveState)
        {
            
        }
        
        public override void Update()
        {
        }

        public override void LoadContent()
        {
        }

        public override void OnExit(bool isHide = true)
        {
        }

        public override void OnStart()
        {
            throw new System.NotImplementedException();
        }
    }
}