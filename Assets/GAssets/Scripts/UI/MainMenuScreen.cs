using Michsky.MUIP;
using UnityEngine.UI;

public class MainMenuScreen : UIStateBase
{
    public ButtonManager PlayButton;
    public ButtonManager ShopButton;
    public ButtonManager LikeButton;

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
