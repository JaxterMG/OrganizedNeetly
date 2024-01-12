using UnityEngine;

public class ColorsChanger
{
    private FiguresColors _figuresColors;
    public ColorsChanger()
    {
        PlayerPrefs.GetString("FiguresColors", "DefaultTheme");
    }
}
