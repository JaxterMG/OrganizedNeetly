
using Core.StateMachine.Game;
using Core.StateMachine.Menu;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScreen : UIStateBase
{
    [SerializeField] Button _pauseButton;

    public override void OnStart()
    {
        base.OnStart();
        _pauseButton.onClick.AddListener(OnRestartButtonPressed);
    }
    private void OnRestartButtonPressed()
    {
        _gameController.ChangeState(new MenuState(_gameController));
    }
    public override void OnExit(bool isHide = true)
    {
        _pauseButton.onClick.RemoveListener(OnRestartButtonPressed);
        base.OnExit(isHide);
    }
}
