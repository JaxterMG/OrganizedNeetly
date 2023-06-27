using UnityEngine;
using DG.Tweening;

public class LoadingScreen : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public CanvasGroup CanvasGroup => _canvasGroup; 
    private float _timeToFade = 0.5f;
    public void LoadContent()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnStart()
    {
        _canvasGroup.DOFade(1, _timeToFade);
    }
    public void OnExit()
    {
        _canvasGroup.DOFade(0, _timeToFade);
    }
}
