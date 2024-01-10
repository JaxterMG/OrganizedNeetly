using Michsky.MUIP;
using UnityEngine.UI;

public class MainMenuScreen : UIStateBase
{
    public ButtonManager PlayButton;
    public ButtonManager ShopButton;
    public ButtonManager LikeButton;
    public ButtonManager SettingsButton;

    public override void OnStart(params ButtonManager[] buttonManagers)
    {
        base.OnStart(PlayButton, ShopButton, LikeButton, SettingsButton);
    }


    public override void OnExit(bool isHide = true, params ButtonManager[] buttonManagers)
    {
        base.OnExit(isHide, PlayButton, ShopButton, LikeButton, SettingsButton);
    }
}
