using UnityEngine;
using DG.Tweening;
using Michsky.MUIP;

public class SettingsScreen : UIStateBase
{
    public ButtonManager BackButton;
 
    public override void OnStart(params ButtonManager[] buttonManagers)
    {
        _canvasGroup.DOFade(1, 0);
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;

        RectTransform shopRect = GetComponent<RectTransform>();
        shopRect.DOAnchorPos(new Vector2(0, ScreenAnchorsController.Instance.DownAnchor.localPosition.y * 2), 0);
        shopRect.DOAnchorPos(new Vector2(0, 0), 1);
        AddButtonsListerToPublishClickSounds(BackButton);
    }


    public override void OnExit(bool isHide = true, params ButtonManager[] buttonManagers)
    {
        BackButton.onClick.RemoveAllListeners();
        RectTransform shopRect = GetComponent<RectTransform>();
        shopRect.DOAnchorPos(new Vector2(0, ScreenAnchorsController.Instance.DownAnchor.localPosition.y * 2), 1);
        base.OnExit(false, BackButton);
    }
}
