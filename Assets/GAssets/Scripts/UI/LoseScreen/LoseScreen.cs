using UnityEngine.UI;

namespace UI
{
    public class LoseScreen : UIStateBase
    {
        public Button MainMenuButton;
        public Button ShopButton;
        public Button ContinueButton;
        public Button RestartButton;

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
