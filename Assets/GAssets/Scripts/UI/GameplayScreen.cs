using Michsky.MUIP;

public class GameplayScreen : UIStateBase
{
    public ButtonManager PauseButton;
    
    
    public override void OnExit(bool isHide = true)
    {
        PauseButton.onClick.RemoveAllListeners();
        base.OnExit(isHide);
    }
}
