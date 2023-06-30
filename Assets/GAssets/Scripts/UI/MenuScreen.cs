
using Core.StateMachine.Game;
using Core.StateMachine.Loading;
using Core.StateMachine.Menu;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : UIStateBase
{
    public Button MainMenuButton;
    public Button ShopButton;
    public Button ContinueButton;
    public Button RestartButton;

    public override void OnExit(bool isHide = true)
    {
        MainMenuButton.onClick.RemoveAllListeners();
        ShopButton.onClick.RemoveAllListeners();
        ContinueButton.onClick.RemoveAllListeners();
        RestartButton.onClick.RemoveAllListeners();
        base.OnExit(isHide);
    }
}
