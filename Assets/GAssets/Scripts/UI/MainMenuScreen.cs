using Michsky.MUIP;
using UnityEngine.UI;

public class MainMenuScreen : UIStateBase
{
    public ButtonManager PlayButton;
    public ButtonManager ShopButton;
    public ButtonManager LikeButton;

    public override void OnStart(params ButtonManager[] buttonManagers)
    {
        base.OnStart(PlayButton, ShopButton, LikeButton);
    }


    public override void OnExit(bool isHide = true, params ButtonManager[] buttonManagers)
    {
        base.OnExit(isHide, PlayButton, ShopButton, LikeButton);
    }
}
