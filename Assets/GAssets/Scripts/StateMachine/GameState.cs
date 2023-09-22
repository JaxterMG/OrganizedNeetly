using Core.Controllers;
using Core.StateMachine.Menu;
using UnityEngine;

namespace Core.StateMachine.Game
{

    public class GameState : State
    {
        private GameplayScreen _gameplayScreen;
        private Grid _grid;
        public GameState(GameController gameController, bool isAdditiveState = false) : base(gameController, isAdditiveState)
        {


        }

        public override void Update()
        {
            //     _dragAndDrop.OnUpdate();
            //     _gridHighlighter.OnUpdate();
        }

        public void EndGame()
        {
        }

        public override void LoadContent()
        {
            _grid = GameObject.FindAnyObjectByType<Grid>();
            _gameplayScreen = GameObject.FindAnyObjectByType<GameplayScreen>();
            _gameplayScreen.LoadContent();
            GameObject.FindAnyObjectByType<FiguresSpawner>().OnInititalize();

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


