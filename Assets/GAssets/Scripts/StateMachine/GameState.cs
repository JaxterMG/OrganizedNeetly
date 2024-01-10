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
        public GameState(EventBus eventBus, GameController gameController, bool isAdditiveState = false) : base(eventBus, gameController, isAdditiveState)
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

        }
        private void OnPauseButtonPressed()
        {
            _gameController.CreateAdditiveState(new MenuState(_eventBus, _gameController, true));
        }
        public void OnGameFail()
        {
            _gameController.CreateAdditiveState(new LoseState(_eventBus, _gameController, true));
        }
        public override void OnExit(bool isHide = true)
        {
            _grid.Fail -= OnGameFail;
            _figuresSpawner.ClearFigures();
            _grid.ClearGrid();
            
            _gameplayScreen.OnExit();
        }

        public override void OnStart()
        {
            _gameplayScreen.PauseButton.onClick.AddListener(OnPauseButtonPressed);
            _grid.OnInitialize();
            _gameplayScreen.OnStart();
            _grid.Fail += OnGameFail;
        }
    }
}


