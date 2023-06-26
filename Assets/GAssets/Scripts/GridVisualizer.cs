using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    public int gridWidth;
    public int gridHeight;
    public float cellWidth;
    public float cellHeight;
    public GameObject cellPrefab;  // This should be a prefab with a SpriteRenderer.

    void Start()
    {
        float startX = -(gridWidth * cellWidth) / 2 + cellWidth / 2;
        float startY = -(gridHeight * cellHeight) / 2 + cellHeight / 2;

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Vector3 position = new Vector3(startX + x * cellWidth, startY + y * cellHeight, 0);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);
                cell.transform.parent = this.transform;  // Set the cell's parent to the grid.
            }
        }
    }
}