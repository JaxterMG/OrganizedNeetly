using UnityEngine;
using DG.Tweening;

public class ShopScreen : UIStateBase
{
    public override void OnStart()
    {
        _canvasGroup.DOFade(1, 0);
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;

        RectTransform shopRect = GetComponent<RectTransform>();
        Debug.Log($"{Screen.currentResolution}");
        shopRect.DOAnchorPos(new Vector2(0, ScreenAnchorsController.Instance.DownAnchor.localPosition.y * 2), 0);
        shopRect.DOAnchorPos(new Vector2(0, 0), 1);
    }
    public override void OnExit(bool isHide = true)
    {
        RectTransform shopRect = GetComponent<RectTransform>();
        shopRect.DOAnchorPos(new Vector2(0, ScreenAnchorsController.Instance.DownAnchor.localPosition.y * 2), 1);
        base.OnExit(false);
    }
}
