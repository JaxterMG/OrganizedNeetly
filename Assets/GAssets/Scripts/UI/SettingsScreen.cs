using Core.UI.ScreenController;
using UnityEngine;
using DG.Tweening;
using Michsky.MUIP;

namespace Core.UI
{
    public class SettingsScreen : UIStateBase
    {
        public ButtonManager BackButton;

        [SerializeField] float _animationAmplitude;
        [SerializeField] float _animationPeriod;

        public override void OnStart(params ButtonManager[] buttonManagers)
        {
            _canvasGroup.DOFade(1, 0);
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;

            RectTransform shopRect = GetComponent<RectTransform>();
            shopRect.DOAnchorPos(new Vector2(0, ScreenAnchorsController.Instance.DownAnchor.localPosition.y * 2), 0)
                .SetEase(Ease.InBounce);
            shopRect.DOAnchorPos(new Vector2(0, 0), 0.5f)
                .SetEase(Ease.OutElastic, _animationAmplitude, _animationPeriod);
            AddButtonsListerToPublishClickSounds(BackButton);
        }


        public override void OnExit(bool isHide = true, params ButtonManager[] buttonManagers)
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            BackButton.onClick.RemoveAllListeners();
            RectTransform shopRect = GetComponent<RectTransform>();
            shopRect.DOAnchorPos(new Vector2(0, ScreenAnchorsController.Instance.DownAnchor.localPosition.y * 2), 0.5f)
                .SetEase(Ease.InElastic, _animationAmplitude, _animationPeriod);
            base.OnExit(false, BackButton);
        }
    }
}
