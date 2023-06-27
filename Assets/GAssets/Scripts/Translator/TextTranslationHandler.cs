using UnityEngine;
using TMPro;

namespace Core.Localization.TextComponent
{
    public class TextTranslationHandler : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _text.text = CSVTranslationsReader.RequestTranslation(_text.text);
        }
    }
}
