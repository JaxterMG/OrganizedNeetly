using Core.Shop.Item;
using Core.UI.ScreenController;
using UnityEngine;
using DG.Tweening;
using Michsky.MUIP;

namespace Core.UI
{
    public class ShopScreen : UIStateBase
    {
        public ButtonManager BackButton;
        public GameObject ShopScreenHolder;
        public ShopItem[] ShopItems;

        public override void OnStart(params ButtonManager[] buttonManagers)
        {
            ShopScreenHolder.SetActive(true);
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
            shopRect.DOAnchorPos(new Vector2(0, ScreenAnchorsController.Instance.DownAnchor.localPosition.y * 2), 1)
                .OnComplete(() => ShopScreenHolder.SetActive(false));
            base.OnExit(false, BackButton);
        }
    }
}
