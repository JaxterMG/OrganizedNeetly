using UnityEngine;
using DG.Tweening;
using Core.Controllers;

public abstract class UIStateBase : MonoBehaviour
{
    protected CanvasGroup _canvasGroup;
    protected float _timeToFade = 0.5f;
    protected GameController _gameController;
    public virtual void LoadContent(GameController gameController = null)
    {
        _gameController = gameController;
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public virtual void OnStart()
    {
        if(_canvasGroup == null) return;

        _canvasGroup.DOFade(1, _timeToFade);
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
    }
    public virtual void OnExit(bool isHide = true)
    {
        if(_canvasGroup == null) return;
        if(!isHide) return;

        _canvasGroup.DOFade(0, _timeToFade);
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }
}
