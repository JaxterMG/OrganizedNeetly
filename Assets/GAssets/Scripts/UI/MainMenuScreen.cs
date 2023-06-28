
using Core.StateMachine.Game;
using Core.StateMachine.Loading;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : UIStateBase
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _shopButton;
    [SerializeField] Button _likeButton;

    public override void OnStart()
    {
        base.OnStart();
        _playButton.onClick.AddListener(OnPlayButtonPressed);
        _shopButton.onClick.AddListener(OnShopButtonPressed);
        _likeButton.onClick.AddListener(OnLikeButtonPressed);
    }
    private void OnPlayButtonPressed()
    {
        _gameController.ChangeState(new GameState(_gameController));
    }
    private void OnShopButtonPressed()
    {
        _gameController.ChangeState(new ShopState(_gameController), false);
    }
    private void OnLikeButtonPressed()
    {
        //_gameController.ChangeState(new GameState(_gameController));
    }
    public override void OnExit(bool isHide = true)
    {
        _playButton.onClick.RemoveListener(OnPlayButtonPressed);
        _shopButton.onClick.RemoveListener(OnShopButtonPressed);
        _likeButton.onClick.RemoveListener(OnLikeButtonPressed);
        base.OnExit(isHide);
    }
}
