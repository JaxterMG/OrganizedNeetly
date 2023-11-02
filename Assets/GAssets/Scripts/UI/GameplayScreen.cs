using UnityEngine.UI;

public class GameplayScreen : UIStateBase
{
    public Button PauseButton;
    
    
    public override void OnExit(bool isHide = true)
    {
        PauseButton.onClick.RemoveAllListeners();
        base.OnExit(isHide);
    }
}
