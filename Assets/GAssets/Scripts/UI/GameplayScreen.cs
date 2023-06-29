using Core.StateMachine.Menu;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScreen : UIStateBase
{
    [SerializeField] Button _pauseButton;
    
    private MenuState _menuState;
    public override void OnStart()
    {
        base.OnStart();
        _pauseButton.onClick.AddListener(OnPauseButtonPressed);
    }
    private void OnPauseButtonPressed()
    {
        //_gameController.ChangeState(new MenuState(_gameController));
        _menuState = new MenuState(_gameController);
        _gameController.CreateAdditiveState(_menuState, true);
        _menuState.LoadContent();
        _menuState.OnStart();
    }
    public override void OnExit(bool isHide = true)
    {
        _menuState.OnExit();
        _pauseButton.onClick.RemoveListener(OnPauseButtonPressed);

        base.OnExit(isHide);
    }
}
