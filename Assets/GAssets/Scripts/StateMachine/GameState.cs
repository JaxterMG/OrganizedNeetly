using Core.Controllers;

namespace Core.StateMachine.Game
{
    public class GameState : State
    {
        public GameState(GameController gameController) : base(gameController)
        {
        }

        public override void Update()
        {
        }

        public void EndGame()
        {
        }

        public override void LoadContent()
        {
        }

        public override void OnExit()
        {
        }
    }
}