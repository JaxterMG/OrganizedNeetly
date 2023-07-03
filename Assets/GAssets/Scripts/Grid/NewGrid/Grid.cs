using UnityEngine;

public class Grid : MonoBehaviour, IProvidable
{
    public GameObject gridCellPrefab;  // Prefab for the grid cell
    public int gridRows = 10;  // Number of rows
    public int gridColumns = 10;  // Number of columns
    public float gapSize = 0.1f; // Size of the gap between cells
    public float cellSize = 1f;
    private GameObject[,] grid;  // The grid represented as a 2D array


    public void OnInitialize()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Transform gridHolder = new GameObject("GridHolder").transform;
        gridHolder.position = CalculateGridCenter();
        grid = new GameObject[gridRows, gridColumns];

        for (int i = 0; i < gridRows; i++)
        {
            for (int j = 0; j < gridColumns; j++)
            {
                // Calculate the position for this cell
                Vector3 cellPosition = new Vector3((j * (cellSize + gapSize)), (i * (cellSize + gapSize)), 0);

                // Instantiate the cell prefab at this position
                GameObject cell = Instantiate(gridCellPrefab, cellPosition, Quaternion.identity);

                // Set the cell's size
                cell.transform.localScale = new Vector3(cellSize, cellSize, cellSize);

                grid[i, j] = cell;
                cell.transform.SetParent(gridHolder);
            }
        }
        gridHolder.position = Vector3.zero;
    }
    Vector3 CalculateGridCenter()
    {
        // Calculate the total width and height of the grid
        float totalWidth = (gridColumns * cellSize) + ((gridColumns - 1) * gapSize);
        float totalHeight = (gridRows * cellSize) + ((gridRows - 1) * gapSize);

        // The center point is at half the total width and height, accounting for the pivot in the center of the cells
        Vector3 center = new Vector3(totalWidth / 2f - cellSize / 2f, totalHeight / 2f - cellSize / 2f, 0);

        return center;
    }




    public void OnUpdate()
    {
    }
}