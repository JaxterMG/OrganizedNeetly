using Michsky.MUIP;
using UnityEngine.UI;

public class MenuScreen : UIStateBase
{
    public ButtonManager MainMenuButton;
    public ButtonManager ShopButton;
    public ButtonManager ContinueButton;
    public ButtonManager RestartButton;

    public override void OnStart(params ButtonManager[] buttonManagers)
    {
        base.OnStart(MainMenuButton, ShopButton, ContinueButton, RestartButton);
    }


    public override void OnExit(bool isHide = true, params ButtonManager[] buttonManagers)
    {
        base.OnExit(isHide, MainMenuButton, ShopButton, ContinueButton, RestartButton);
    }
}
