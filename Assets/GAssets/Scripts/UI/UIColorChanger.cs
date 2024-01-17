using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIColorChanger : MonoBehaviour
{
    [Inject] private EventBus _eventBus;
    [SerializeField] private string _uiElementName;
    [SerializeField] private Image[] _uiImages;
    private void OnEnable()
    {
        _eventBus.Subscribe<Dictionary<string, Color>>(EventType.ChangeUIColor, ChangeUIColor);
    }
    void OnDisable()
    {
        _eventBus.Unsubscribe<Dictionary<string, Color>>(EventType.ChangeUIColor, ChangeUIColor);
    }

    private void ChangeUIColor(Dictionary<string, Color> uiColors)
    {
        if(!uiColors.ContainsKey(_uiElementName)) return;
        
        foreach (var uiImage in _uiImages)
        {
            uiImage.color = uiColors[_uiElementName];
        }
    }

}
