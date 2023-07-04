using UnityEngine;

public class Grid : MonoBehaviour, IProvidable
{
    public Cell gridCellPrefab;  // Prefab for the grid cell
    public int gridRows = 10;  // Number of rows
    public int gridColumns = 10;  // Number of columns
    public float gapSize = 0.1f; // Size of the gap between cells
    public float cellSize = 1f;
    private Cell[,] grid;  // The grid represented as a 2D array


    public void OnInitialize()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Transform gridHolder = new GameObject("GridHolder").transform;
        gridHolder.position = CalculateGridCenter();
        grid = new Cell[gridRows, gridColumns];

        for (int i = 0; i < gridRows; i++)
        {
            for (int j = 0; j < gridColumns; j++)
            {
                // Calculate the position for this cell
                Vector3 cellPosition = new Vector3((j * (cellSize + gapSize)), (i * (cellSize + gapSize)), 0);

                // Instantiate the cell prefab at this position
                Cell cell = Instantiate(gridCellPrefab, cellPosition, Quaternion.identity);

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

    public bool TryPlaceFigure(FigureDragHandler figureDragHandler)
    {
        // Calculate the grid coordinates for the figure based on its pivot (parent's position)
        Vector3 figurePosition = figureDragHandler.transform.position;
        Vector2Int gridPivotPosition = WorldToGrid(figurePosition);

        // Get all cells in the figure
        Cell[] figureCells = figureDragHandler.GetComponentsInChildren<Cell>();

        // Iterate over each cell in the figure
        foreach (var cell in figureCells)
        {
            // Calculate the relative position of each cell to the pivot in terms of grid cells
            Vector3 relativePosition = cell.transform.position - figurePosition;
            Vector2Int relativeGridPosition = WorldToGrid(relativePosition);

            // Calculate the absolute position of the cell in the grid
            Vector2Int gridPosition = gridPivotPosition + relativeGridPosition;

            // Check if the calculated position is outside the grid
            if (!IsWithinGrid(gridPosition))
                return false;

            // Check if the cell in the grid is empty
            if (grid[gridPosition.x, gridPosition.y] != null)
                return false;
        }

        // If none of the checks failed, place the figure on the grid
        foreach (var cell in figureCells)
        {
            Vector3 relativePosition = cell.transform.position - figurePosition;
            Vector2Int relativeGridPosition = WorldToGrid(relativePosition);

            Vector2Int gridPosition = gridPivotPosition + relativeGridPosition;
            grid[gridPosition.x, gridPosition.y] = cell;
            cell.transform.position = GridToWorld(gridPosition);  // Adjust cell position to the center of the grid cell
        }

        return true;
    }

    private Vector2Int WorldToGrid(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x / (cellSize + gapSize));
        int y = Mathf.RoundToInt(worldPosition.y / (cellSize + gapSize));
        return new Vector2Int(x, y);
    }

    // Converts grid coordinates to world position
    private Vector3 GridToWorld(Vector2Int gridPosition)
    {
        float x = gridPosition.x * (cellSize + gapSize);
        float y = gridPosition.y * (cellSize + gapSize);
        return new Vector3(x, y, 0f);
    }

    private bool IsWithinGrid(Vector2Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < gridRows &&
               gridPosition.y >= 0 && gridPosition.y < gridColumns;
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}