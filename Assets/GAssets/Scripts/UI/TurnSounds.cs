using Michsky.MUIP;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TurnSounds : MonoBehaviour
{
    [Inject] EventBus _eventBus;
    [SerializeField] ButtonManager _soundButton;
    [SerializeField] Image _crossImage;

    private bool _isTurnedOff;
    void OnEnable()
    {
        _soundButton.onClick.AddListener(ChangeSounds);
    }
    void OnDisable()
    {
        _soundButton.onClick.RemoveListener(ChangeSounds);
    }
    public void ChangeSounds()
    {
        _isTurnedOff = !_isTurnedOff;
        _eventBus.Publish<bool>(EventType.ChangeSoundsVolume, _isTurnedOff);
        _crossImage.enabled = _isTurnedOff ? true : false;
    }
}
