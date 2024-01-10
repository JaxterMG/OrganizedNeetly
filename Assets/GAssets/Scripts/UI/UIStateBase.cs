using UnityEngine;
using DG.Tweening;
using Zenject;
using Michsky.MUIP;

public abstract class UIStateBase : MonoBehaviour
{
    [Inject] EventBus _eventBus;
    
    protected CanvasGroup _canvasGroup;
    protected float _timeToFade = 0.5f;
    public virtual void LoadContent()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public virtual void OnStart(params ButtonManager[] buttonManagers)
    {
        AddButtonsListerToPublishClickSounds(buttonManagers);
        if(_canvasGroup == null) return;

        _canvasGroup.DOFade(1, _timeToFade);
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
    }
    
    public virtual void OnExit(bool isHide = true, params ButtonManager[] buttonManagers)
    {
        foreach (var buttonManager in buttonManagers)
        {
            buttonManager?.onClick.RemoveAllListeners();
        }

        if(_canvasGroup == null) return;
        if(!isHide) return;

        _canvasGroup.DOFade(0, _timeToFade);
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }
    public void AddButtonsListerToPublishClickSounds(params ButtonManager[] buttonManagers)
    {
        foreach (var buttonManager in buttonManagers)
        {
            buttonManager?.onClick.AddListener(() => _eventBus.Publish<string>(EventType.PlaySound, "UIClick"));
        }
    }
}
