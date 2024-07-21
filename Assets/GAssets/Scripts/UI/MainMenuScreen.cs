using Michsky.MUIP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class MainMenuScreen : UIStateBase
    {
        public ButtonManager PlayButton;
        public ButtonManager ShopButton;
        public ButtonManager LikeButton;
        public ButtonManager SettingsButton;
        [SerializeField] private TextMeshProUGUI _score;

        public void ShowScore(int score)
        {
            _score.text = score.ToString();
        }

        public override void OnStart(params ButtonManager[] buttonManagers)
        {
            base.OnStart(PlayButton, ShopButton, LikeButton, SettingsButton);
        }


        public override void OnExit(bool isHide = true, params ButtonManager[] buttonManagers)
        {
            base.OnExit(isHide, PlayButton, ShopButton, LikeButton, SettingsButton);
        }
    }
}
