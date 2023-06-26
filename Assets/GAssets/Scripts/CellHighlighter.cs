using UnityEngine;

public class CellHighlighter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Highlight(bool state)
    {
        if (state)
        {
            spriteRenderer.color = Color.red;  // Highlight the cell in red.
        }
        else
        {
            spriteRenderer.color = Color.white;  // Restore the original color.
        }
    }
}