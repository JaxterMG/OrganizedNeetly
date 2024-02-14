using System.Threading.Tasks;
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
            _gameplayScreen.ShowScore(_gameController.ScoreController.GetPoints());
            _gameplayScreen.LoadContent();
            _figuresSpawner = GameObject.FindAnyObjectByType<FiguresSpawner>();
        }
        private void OnPauseButtonPressed()
        {
            _gameController.CreateAdditiveState(new MenuState(_eventBus, _gameController, true));
        }
        public void OnGameFail(IScoreController scoreController)
        {
            ReviveState reviveState = new ReviveState(_eventBus, _gameController, true);
            reviveState.LinkScoreController(scoreController);
            //LoseState loseState = new LoseState(_eventBus, _gameController, true);
            //loseState.LinkScoreController(scoreController);
            //_gameController.CreateAdditiveState(loseState);
            _gameController.CreateAdditiveState(reviveState);
        }
        public override async void OnStart()
        {
            _eventBus.Subscribe<int>(EventType.Revive, _figuresSpawner.SpawnReviveFigures);
            _gameplayScreen.PauseButton.onClick.AddListener(OnPauseButtonPressed);
            _grid.OnInitialize();

            await Task.Delay(200);

            _figuresSpawner.OnInititalize();
            _gameplayScreen.OnStart();
            _grid.Fail += OnGameFail;
        }
        public override void OnExit(bool isHide = true)
        {
            _eventBus.Unsubscribe<int>(EventType.Revive, _figuresSpawner.SpawnReviveFigures);
            
            _grid.Fail -= OnGameFail;
            _figuresSpawner.ClearFigures();
            _grid.ClearGrid();

            _gameplayScreen.OnExit();
        }
    }
}


