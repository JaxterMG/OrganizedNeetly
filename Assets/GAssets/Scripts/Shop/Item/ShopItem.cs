using Michsky.MUIP;
using TMPro;
using UnityEngine;
using Zenject;

public class ShopItem : MonoBehaviour
{
    [Inject] private EventBus _eventBus;
    [Inject] private ColorsChanger _colorsChanger;
    [SerializeField] private string _theme;
    [SerializeField] int _cost;

    [SerializeField] private FiguresColors _figuresColor;
    [SerializeField] private FieldColor _fieldColor;
    [SerializeField] private BackGroundColor _backGroundColor;
    [SerializeField] private UIColors _uiColors;

    [SerializeField] ButtonManager _buyButton;
    [SerializeField] private TextMeshProUGUI _themeText;

    void Awake()
    {
        _themeText.text = _theme;
        _buyButton.onClick.AddListener(OnButtonClicked);

        if(PlayerPrefs.GetInt(_theme, 0) == 0)
        {
            _buyButton.SetText($"{_cost}");
        }
        else
        {
            _buyButton.SetText($"Use");
            _buyButton.enableIcon = false;
        }
    }
    private void OnButtonClicked()
    {
        if(PlayerPrefs.GetInt(_theme, 0) == 0)
        {
            PlayerPrefs.SetInt(_theme, 1);
            _buyButton.enableIcon = false;
            _buyButton.SetText($"Use");
        }
        _colorsChanger.ChangeTheme(_theme);
    }

    void OnDestroy()
    {
        _buyButton.onClick.RemoveListener(OnButtonClicked);
    }
}
