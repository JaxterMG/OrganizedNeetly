using Michsky.MUIP;
using UnityEngine.UI;

namespace UI
{
    public class LoseScreen : UIStateBase
    {
        public ButtonManager MainMenuButton;
        public ButtonManager ShopButton;
        public ButtonManager ContinueButton;
        public ButtonManager RestartButton;

        public override void OnExit(bool isHide = true)
        {
            MainMenuButton.onClick.RemoveAllListeners();
            ShopButton.onClick.RemoveAllListeners();
            ContinueButton.onClick.RemoveAllListeners();
            RestartButton.onClick.RemoveAllListeners();
            base.OnExit(isHide);
        }
        
    }
}
