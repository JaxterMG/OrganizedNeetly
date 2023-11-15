using Michsky.MUIP;
using UnityEngine.UI;

public class MenuScreen : UIStateBase
{
    public ButtonManager MainMenuButton;
    public ButtonManager ShopButton;
    public ButtonManager ContinueButton;
    public ButtonManager RestartButton;

    public override void OnExit(bool isHide = true)
    {
        MainMenuButton.onClick.RemoveAllListeners();
        ShopButton.onClick.RemoveAllListeners();
        ContinueButton.onClick.RemoveAllListeners();
        RestartButton.onClick.RemoveAllListeners();
        base.OnExit(isHide);
    }
}
