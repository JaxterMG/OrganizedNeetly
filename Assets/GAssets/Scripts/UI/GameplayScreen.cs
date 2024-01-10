using Michsky.MUIP;
using UnityEditor;
using Zenject;

public class GameplayScreen : UIStateBase
{
    [Inject] EventBus _eventBus;
    public ButtonManager PauseButton;

    public override void OnStart(params ButtonManager[] buttonManagers)
    {
        base.OnStart(PauseButton);
    }


    public override void OnExit(bool isHide = true, params ButtonManager[] buttonManagers)
    {
        base.OnExit(isHide, PauseButton);
    }
}
