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
        //string backGroundColorKey = PlayerPrefs.GetString("BackGroundColor", "BackGroundColorDefaultTheme");
        string backGroundColorKey = "SoftBackGround";
        LoadBackGroundColor(backGroundColorKey);

        //string addressableKey = PlayerPrefs.GetString("FiguresColors", "DefaultTheme");
        string addressableKey = "SoftTheme";
        LoadFiguresColors(addressableKey);

        string uiColors = "SoftUI";
        //string uiColors = PlayerPrefs.GetString("UIColors", "DefaultUI");
        LoadUIColors(uiColors);
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
}
