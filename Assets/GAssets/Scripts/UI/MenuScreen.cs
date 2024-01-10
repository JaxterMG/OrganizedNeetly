using Michsky.MUIP;

public class MenuScreen : UIStateBase
{
    public ButtonManager MainMenuButton;
    public ButtonManager ShopButton;
    public ButtonManager ContinueButton;
    public ButtonManager RestartButton;
    public ButtonManager SettingsButton;

    public override void OnStart(params ButtonManager[] buttonManagers)
    {
        base.OnStart(MainMenuButton, ShopButton, ContinueButton, RestartButton, SettingsButton);
    }

    public override void OnExit(bool isHide = true, params ButtonManager[] buttonManagers)
    {
        base.OnExit(isHide, MainMenuButton, ShopButton, ContinueButton, RestartButton, SettingsButton);
    }
}
