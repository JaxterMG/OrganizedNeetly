using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Core.StateMachine.Menu;
using Core.Controllers;

public class ShopScreen : UIStateBase
{
    [SerializeField] Button _backButton;
    
    public override void OnStart()
    {
        _backButton.onClick.AddListener(OnBackButtonPressed);

        _canvasGroup.DOFade(1, 0);
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;

        RectTransform shopRect = GetComponent<RectTransform>();
        Debug.Log($"{Screen.currentResolution}");
        shopRect.DOAnchorPos(new Vector2(0, ScreenAnchorsController.Instance.DownAnchor.localPosition.y * 2), 0);
        shopRect.DOAnchorPos(new Vector2(0, 0), 1);
    }

    private void OnBackButtonPressed()
    {
        _gameController.ChangeState(new MainMenuState(_gameController), false);
    }

    public override void OnExit(bool isHide = true)
    {
        _backButton.onClick.RemoveListener(OnBackButtonPressed);
        RectTransform shopRect = GetComponent<RectTransform>();
        shopRect.DOAnchorPos(new Vector2(0, ScreenAnchorsController.Instance.DownAnchor.localPosition.y * 2), 1);
        base.OnExit(false);
    }
}
