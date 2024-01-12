using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ColorsChanger
{
    private FiguresColors _figuresColors;
    public ColorsChanger()
    {
        string addressableKey = PlayerPrefs.GetString("FiguresColors", "DefaultTheme");
        LoadFiguresColors(addressableKey);
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
}
