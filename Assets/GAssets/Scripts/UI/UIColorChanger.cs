using System.Collections.Generic;
using Core.EventBus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Core.EventBus;

namespace Core.UI
{
    public class UIColorChanger : MonoBehaviour
    {
        [Inject] private EventBus.EventBus _eventBus;
        [SerializeField] private string _uiElementName;
        [SerializeField] private Image[] _uiImages;

        private void OnEnable()
        {
            _eventBus.Subscribe<Dictionary<string, Color>>(BusEventType.ChangeUIColor, ChangeUIColor);
        }

        void OnDisable()
        {
            _eventBus.Unsubscribe<Dictionary<string, Color>>(BusEventType.ChangeUIColor, ChangeUIColor);
        }

        private void ChangeUIColor(Dictionary<string, Color> uiColors)
        {
            if (!uiColors.ContainsKey(_uiElementName)) return;

            foreach (var uiImage in _uiImages)
            {
                uiImage.color = uiColors[_uiElementName];
            }
        }

    }
}
