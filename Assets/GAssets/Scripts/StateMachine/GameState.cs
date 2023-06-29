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
        private GameplayScreen _gameplayScreen;
        public GameState(GameController gameController) : base(gameController)
        {
            
        
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
            _gameplayScreen = GameObject.FindAnyObjectByType<GameplayScreen>();
            _gameplayScreen.LoadContent(_gameController);

            _dragAndDrop = GameObject.FindAnyObjectByType<DragAndDrop>();
            _dragAndDrop.OnInitialize();

            _gridVisualizer = GameObject.FindAnyObjectByType<GridVisualizer>();
            _gridVisualizer.OnInitialize();

            _gridHighlighter = GameObject.FindAnyObjectByType<GridHighlighter>();
            _gridVisualizer.OnInitialize();
            
        }

        public override void OnExit(bool isHide = true)
        {
            _gameplayScreen.OnExit();
            
        }

        public override void OnStart()
        {
            _gameplayScreen.OnStart();
        }
    }
}