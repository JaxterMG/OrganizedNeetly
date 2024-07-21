using Michsky.MUIP;
using TMPro;
using UnityEngine;
using Zenject;
using Core.EventBus;

namespace Core.UI
{
    public class GameplayScreen : UIStateBase
    {
        [Inject] EventBus.EventBus _eventBus;
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
}
