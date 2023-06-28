using Core.Controllers;
using Core.GridElements;
using UnityEngine;

namespace Core.StateMachine.Game
{
    public class GameState : State
    {
        private GridHighlighter _gridHighlighter;
        private DragAndDrop _dragAndDrop;
        private GridVisualizer _gridVisualizer;
        public GameState(GameController gameController) : base(gameController)
        {
            _dragAndDrop = GameObject.FindAnyObjectByType<DragAndDrop>();
            _dragAndDrop.OnInitialize();

            _gridVisualizer = GameObject.FindAnyObjectByType<GridVisualizer>();
            _gridVisualizer.OnInitialize();

            _gridHighlighter = GameObject.FindAnyObjectByType<GridHighlighter>();
            _gridVisualizer.OnInitialize();
        
        }

        public override void Update()
        {
            _dragAndDrop.OnUpdate();
            _gridHighlighter.OnUpdate();
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

        public override void OnStart()
        {
        }
    }
}