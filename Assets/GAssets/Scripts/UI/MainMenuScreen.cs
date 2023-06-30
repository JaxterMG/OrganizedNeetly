
using Core.StateMachine.Game;
using Core.StateMachine.Loading;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : UIStateBase
{
    public Button PlayButton;
    public Button ShopButton;
    public Button LikeButton;

    public override void OnStart()
    {
        base.OnStart();
    }
    
    public override void OnExit(bool isHide = true)
    {
        PlayButton.onClick.RemoveAllListeners();
        ShopButton.onClick.RemoveAllListeners();
        LikeButton.onClick.RemoveAllListeners();
        base.OnExit(isHide);
    }
}
