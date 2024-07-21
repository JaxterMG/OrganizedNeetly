using UnityEngine;
using TMPro;

namespace Core.Translator
{
    public class TextTranslationHandler : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _text.text = CSVTranslationsReader.RequestTranslation(_text.text);            
        }
    }
}
