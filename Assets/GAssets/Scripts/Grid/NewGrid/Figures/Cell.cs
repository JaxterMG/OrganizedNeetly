using UnityEngine;

public class Cell : MonoBehaviour
{
    public string FigureName;
    private Color _cellColor;

    public void SetColor(Color color)
    {
        _cellColor = color;
        SpriteRenderer spriteRenderer =  GetComponent<SpriteRenderer>();
        spriteRenderer.color = _cellColor;
    } 
}
