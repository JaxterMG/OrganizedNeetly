using Michsky.MUIP;
using TMPro;
using UnityEngine;
using Zenject;

public class GameplayScreen : UIStateBase
{
    [Inject] EventBus _eventBus;
    public ButtonManager PauseButton;
    [SerializeField] private TextMeshProUGUI _score;

    public void ShowScore(int score)
    {
        _score.text = score.ToString();
    }
    public override void OnStart(params ButtonManager[] buttonManagers)
    {
        base.OnStart(PauseButton);
    }


    public override void OnExit(bool isHide = true, params ButtonManager[] buttonManagers)
    {
        base.OnExit(isHide, PauseButton);
    }
}
