using Core.Controllers;
using Core.StateMachine.Menu;
using UnityEngine;

namespace Core.StateMachine.Game
{

    public class GameState : State
    {
        private GameplayScreen _gameplayScreen;
        private Grid _grid;
        private FiguresSpawner _figuresSpawner;
        public GameState(GameController gameController, bool isAdditiveState = false) : base(gameController, isAdditiveState)
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
            _grid = GameObject.FindAnyObjectByType<Grid>();
            _gameplayScreen = GameObject.FindAnyObjectByType<GameplayScreen>();
            _gameplayScreen.LoadContent();
            _figuresSpawner = GameObject.FindAnyObjectByType<FiguresSpawner>();
            _figuresSpawner.OnInititalize();

            //     _dragAndDrop = GameObject.FindAnyObjectByType<DragAndDrop>();
            //     _dragAndDrop.OnInitialize();

            //     _gridVisualizer = GameObject.FindAnyObjectByType<GridVisualizer>();
            //     _gridVisualizer.OnInitialize();

            //     _gridHighlighter = GameObject.FindAnyObjectByType<GridHighlighter>();
            //     _gridVisualizer.OnInitialize();

        }
        private void OnPauseButtonPressed()
        {
            _gameController.CreateAdditiveState(new MenuState(_gameController, true));
        }

        public override void OnExit(bool isHide = true)
        {
            _figuresSpawner.ClearFigures();
            _grid.ClearGrid();
            
            _gameplayScreen.OnExit();
        }

        public override void OnStart()
        {
            _gameplayScreen.PauseButton.onClick.AddListener(OnPauseButtonPressed);
            _grid.OnInitialize();
            _gameplayScreen.OnStart();
        }
    }
}


