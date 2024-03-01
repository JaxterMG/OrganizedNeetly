using System;
using Michsky.MUIP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopItem : MonoBehaviour
{
    public event Action ThemeChanged; 
    private Image _background;
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

    private int _isPurchased;

    private MoneyManager _moneyManager;

    void Awake()
    {
        _moneyManager = FindObjectOfType<MoneyManager>();
        _background = GetComponent<Image>();
        _background.color = _backGroundColor.Color;
        _themeText.text = _theme;
        _buyButton.onClick.AddListener(OnButtonClicked);
        _isPurchased = PlayerPrefs.GetInt(_theme, 0);
        if(_isPurchased == 0)
        {
            _buyButton.SetText($"{_cost}");
        }
        else
        {
            _buyButton.SetText($"Use");
            _buyButton.enableIcon = false;
        }
        _buyButton?.onClick.AddListener(() => _eventBus.Publish<string>(BusEventType.PlaySound, "UIClick"));
    }
    private void OnButtonClicked()
    {
        if(_isPurchased == 0 &&_moneyManager.GetMoneyCount() < _cost) return;

        if(_isPurchased == 0)
        {
            _isPurchased = 1;
            PlayerPrefs.SetInt(_theme, _isPurchased);
            _eventBus.Publish<int>(BusEventType.SubtractMoney, _cost);
            _buyButton.enableIcon = false;
            _buyButton.SetText($"Use");
        }
        _colorsChanger.ChangeTheme(_theme);
        ThemeChanged?.Invoke();
    }

    void OnDestroy()
    {
        _buyButton.onClick.RemoveListener(OnButtonClicked);
    }
}
