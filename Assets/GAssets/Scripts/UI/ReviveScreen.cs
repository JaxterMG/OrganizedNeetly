using Michsky.MUIP;
using UnityEngine.UI;

namespace Core.UI
{
    public class ReviveScreen : UIStateBase
    {
        public ButtonManager ReviveButton;
        public ButtonManager DeclineButton;
        public Image Radial;

        public override void OnStart(params ButtonManager[] buttonManagers)
        {
            base.OnStart(ReviveButton, DeclineButton);
            Radial.fillAmount = 1;
        }

        public override void OnExit(bool isHide = true, params ButtonManager[] buttonManagers)
        {
            base.OnExit(isHide, ReviveButton, DeclineButton);
        }

    }
}
