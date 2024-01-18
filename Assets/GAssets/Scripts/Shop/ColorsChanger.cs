using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ColorsChanger
{
    private EventBus _eventBus;
    private Camera _camera;
    private FiguresColors _figuresColors;
    private BackGroundColor _backGroundColor;
    public ColorsChanger(EventBus eventBus)
    {
        _eventBus = eventBus;

        _camera = Camera.main;
        string backGroundColorKey = PlayerPrefs.GetString("BackGroundColor", "DefaultBackGroundColor");
        //string backGroundColorKey = "SoftBackGround";
        LoadBackGroundColor(backGroundColorKey);

        string addressableKey = PlayerPrefs.GetString("FiguresColors", "DefaultFiguresColors");
        //string addressableKey = "SoftTheme";
        LoadFiguresColors(addressableKey);

        //string uiColors = "SoftUI";
        string uiColors = PlayerPrefs.GetString("UIColors", "DefaultUIColors");
        LoadUIColors(uiColors);

        //string gridColor = "SoftGrid";
        string gridColor = PlayerPrefs.GetString("GridColor", "DefaultGridColor");
        LoadGridColor(gridColor);

    }
    public void ChangeTheme(string theme)
    {
        string backgroundColor = $"{theme}BackGroundColor";
        string figuresColors = $"{theme}FiguresColors";
        string uiColors = $"{theme}UIColors";
        string gridColor = $"{theme}GridColor";

        PlayerPrefs.SetString("BackGroundColor", backgroundColor);
        LoadBackGroundColor(backgroundColor);

        PlayerPrefs.SetString("FiguresColors", figuresColors);
        LoadFiguresColors(figuresColors);

        PlayerPrefs.SetString("UIColors", uiColors);
        LoadUIColors(uiColors);

        PlayerPrefs.SetString("GridColor", gridColor);
        LoadGridColor(gridColor);
    }

    public FiguresColors GetFiguresColors()
    {
        return _figuresColors;
    }

    private void LoadFiguresColors(string key)
    {
        Addressables.LoadAssetAsync<FiguresColors>(key).Completed += OnFiguresColorsLoaded;
    }

    private void OnFiguresColorsLoaded(AsyncOperationHandle<FiguresColors> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _figuresColors = handle.Result;
        }
        else
        {
            Debug.LogError("Не удалось загрузить FiguresColors с ключом: ");
        }
    }

    private void LoadBackGroundColor(string key)
    {
        Addressables.LoadAssetAsync<BackGroundColor>(key).Completed += OnBackGroundColorLoaded;
    }

    private void OnBackGroundColorLoaded(AsyncOperationHandle<BackGroundColor> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _backGroundColor = handle.Result;
            _camera.backgroundColor = _backGroundColor.Color;
        }
        else
        {
            Debug.LogError("Не удалось загрузить BackGroundColor с ключом: ");
        }
    }

    private void LoadUIColors(string key)
    {
        Addressables.LoadAssetAsync<UIColors>(key).Completed += OnLoadUIColorsLoaded;
    }

    private void OnLoadUIColorsLoaded(AsyncOperationHandle<UIColors> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _eventBus.Publish<Dictionary<string, Color>>(EventType.ChangeUIColor, handle.Result.UI);
        }
        else
        {
            Debug.LogError("Не удалось загрузить UIColors с ключом: ");
        }
    }

    public void LoadGridColor(string key)
    {
        Addressables.LoadAssetAsync<FieldColor>(key).Completed += OnGridColorLoaded;
    }

    private void OnGridColorLoaded(AsyncOperationHandle<FieldColor> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _eventBus.Publish<FieldColor>(EventType.ChangeGridColor, handle.Result);
        }
        else
        {
            Debug.LogError("Не удалось загрузить FieldColor с ключом: ");
        }
    }
}
